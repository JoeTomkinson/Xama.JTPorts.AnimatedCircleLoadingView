using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Views.Animations;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class SideArcsView : ComponentViewAnimation
    {
        private const int MIN_RESIZE_ANGLE = 8;
        private const int MAX_RESIZE_ANGLE = 45;
        private const int INITIAL_LEFT_ARC_START_ANGLE = 100;
        private const int INITIAL_RIGHT_ARC_START_ANGLE = 80;
        private const int MIN_START_ANGLE = 0;
        private const int MAX_START_ANGLE = 165;
        private int startLeftArcAngle = INITIAL_LEFT_ARC_START_ANGLE;
        private int startRightArcAngle = INITIAL_RIGHT_ARC_START_ANGLE;
        private Paint paint;
        private RectF oval;
        private int arcAngle;

        public SideArcsView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            Init();
        }

        private void Init()
        {
            InitPaint();
            arcAngle = MAX_RESIZE_ANGLE;
            InitOval();
        }

        private void InitPaint()
        {
            paint = new Paint
            {
                Color = Resources.GetColor(this.secondaryColor, null),
                StrokeWidth = (strokeWidth)
            };
            paint.SetStyle(Paint.Style.Stroke);
            paint.AntiAlias = (true);
        }

        private void InitOval()
        {
            float padding = paint.StrokeWidth / 2;
            oval = new RectF();
            oval.Set(padding, padding, parentWidth - padding, parentWidth - padding);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawArcs(canvas);
        }

        private void DrawArcs(Canvas canvas)
        {
            canvas.DrawArc(oval, startLeftArcAngle, arcAngle, false, paint);
            canvas.DrawArc(oval, startRightArcAngle, -arcAngle, false, paint);
        }

        public void StartRotateAnimation()
        {
            ValueAnimator valueAnimator = ValueAnimator.OfInt(MIN_START_ANGLE, MAX_START_ANGLE);
            valueAnimator.SetInterpolator(new DecelerateInterpolator());
            valueAnimator.SetDuration(550);
            valueAnimator.Update += (s, e) =>
            {
                startLeftArcAngle = INITIAL_LEFT_ARC_START_ANGLE + (int)e.Animation.AnimatedValue;
                startRightArcAngle = INITIAL_RIGHT_ARC_START_ANGLE - (int)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueAnimator.Start();
        }

        public void StartResizeDownAnimation()
        {
            ValueAnimator valueAnimator = ValueAnimator.OfInt(MAX_RESIZE_ANGLE, MIN_RESIZE_ANGLE);
            valueAnimator.SetInterpolator(new DecelerateInterpolator());
            valueAnimator.SetDuration(620);
            valueAnimator.Update += (s, e) =>
            {
                arcAngle = (int)e.Animation.AnimatedValue;
                Invalidate();
            };
            
            valueAnimator.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.SideArcsResizedTops);
            };
            valueAnimator.Start();
        }
    }
}