using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Views.Animations;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class TopCircleBorderView : ComponentViewAnimation
    {
        private const int MIN_ANGLE = 25;
        private const int MAX_ANGLE = 180;
        private Paint paint;
        private RectF oval;
        private int arcAngle;

        public TopCircleBorderView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            Init();
        }

        private void Init()
        {
            InitPaint();
            InitOval();
            arcAngle = MIN_ANGLE;
        }

        private void InitPaint()
        {
            paint = new Paint
            {
                Color = mainColor,
                StrokeWidth = strokeWidth
            };
            paint.SetStyle(Paint.Style.Stroke);
            paint.AntiAlias = true;
        }

        private void InitOval()
        {
            float padding = paint.StrokeWidth / 2;
            oval = new RectF();
            oval.Set(parentCenter - circleRadius, padding, parentCenter + circleRadius, circleRadius * 2);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawArcs(canvas);
        }

        private void DrawArcs(Canvas canvas)
        {
            canvas.DrawArc(oval, 270, arcAngle, false, paint);
            canvas.DrawArc(oval, 270, -arcAngle, false, paint);
        }

        public void StartDrawCircleAnimation()
        {
            ValueAnimator valueAnimator = ValueAnimator.OfInt(MIN_ANGLE, MAX_ANGLE);
            valueAnimator.SetInterpolator(new DecelerateInterpolator());
            valueAnimator.SetDuration(400);
            valueAnimator.Update += (s, e) =>
            {
                arcAngle = (int)e.Animation.AnimatedValue;
                Invalidate();
            };
            
            valueAnimator.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.MainCircleDrawnTop);
            };

            valueAnimator.Start();
        }
    }
}