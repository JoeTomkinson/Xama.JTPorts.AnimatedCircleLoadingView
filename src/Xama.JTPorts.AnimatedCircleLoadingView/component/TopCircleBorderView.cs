using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Views.Animations;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class TopCircleBorderView : ComponentViewAnimation
    {
        private static int MIN_ANGLE = 25;
        private static int MAX_ANGLE = 180;
        private Paint paint;
        private RectF oval;
        private int arcAngle;

        public TopCircleBorderView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            init();
        }

        private void init()
        {
            initPaint();
            initOval();
            arcAngle = MIN_ANGLE;
        }

        private void initPaint()
        {
            paint = new Paint
            {
                Color = mainColor,
                StrokeWidth = strokeWidth
            };
            paint.SetStyle(Paint.Style.Stroke);
            paint.AntiAlias = true;
        }

        private void initOval()
        {
            float padding = paint.StrokeWidth / 2;
            oval = new RectF();
            oval.Set(parentCenter - circleRadius, padding, parentCenter + circleRadius, circleRadius * 2);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            drawArcs(canvas);
        }

        private void drawArcs(Canvas canvas)
        {
            canvas.DrawArc(oval, 270, arcAngle, false, paint);
            canvas.DrawArc(oval, 270, -arcAngle, false, paint);
        }

        public void startDrawCircleAnimation()
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
                setState(AnimationState.MAIN_CIRCLE_DRAWN_TOP);
            };

            valueAnimator.Start();
        }
    }
}