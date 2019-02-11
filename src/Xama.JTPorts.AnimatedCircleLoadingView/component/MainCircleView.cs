using Android.Animation;
using Android.Content;
using Android.Graphics;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class MainCircleView : ComponentViewAnimation
    {
        private Paint paint;
        private RectF oval;
        private int arcFillAngle = 0;
        private int arcStartAngle = 0;

        public MainCircleView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            init();
        }

        private void init()
        {
            initPaint();
            initOval();
        }

        private void initPaint()
        {
            paint = new Paint();
            paint.SetColorFilter(mainColor);
            paint.StrokeWidth = strokeWidth;
            paint.SetStyle(Paint.Style.FillAndStroke);
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
            canvas.DrawArc(oval, arcStartAngle, arcFillAngle, false, paint);
        }

        public void startFillCircleAnimation()
        {
            ValueAnimator valueAnimator = ValueAnimator.OfInt(90, 360);
            valueAnimator.SetDuration(800);
            valueAnimator.Update += (s, e) =>
            {
                arcStartAngle = (int)e.Animation.AnimatedValue;
                arcFillAngle = (90 - arcStartAngle) * 2;
                Invalidate();
            };
            valueAnimator.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.MAIN_CIRCLE_FILLED_TOP);
            };
            valueAnimator.Start();
        }
    }
}