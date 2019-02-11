using Android.Animation;
using Android.Content;
using Android.Graphics;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class InitialCenterCircleView : ComponentViewAnimation
    {
        private Paint paint;
        private RectF oval;
        private float minRadius;
        private float currentCircleWidth;
        private float currentCircleHeight;

        public InitialCenterCircleView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            init();
        }

        private void init()
        {
            initOval();
            initPaint();
        }

        private void initPaint()
        {
            paint = new Paint();
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = Color.ParseColor(mainColor);
            paint.AntiAlias = true;
        }

        private void initOval()
        {
            oval = new RectF();
            minRadius = (15 * parentWidth) / 700;
            currentCircleWidth = minRadius;
            currentCircleHeight = minRadius;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            drawCircle(canvas);
        }

        public void drawCircle(Canvas canvas)
        {
            RectF oval = new RectF();
            oval.Set(parentCenter - currentCircleWidth, parentCenter - currentCircleHeight,
                parentCenter + currentCircleWidth, parentCenter + currentCircleHeight);
            canvas.DrawOval(oval, paint);
        }

        public void startTranslateTopAnimation()
        {
            float translationYTo = -(255 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", 0, translationYTo);
            translationY.SetDuration(1100);
            translationY.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.MAIN_CIRCLE_TRANSLATED_TOP);
            };
            translationY.Start();
        }

        public void startScaleAnimation()
        {
            ValueAnimator valueAnimator = ValueAnimator.OfFloat(minRadius, circleRadius);
            valueAnimator.SetDuration(1400);
            valueAnimator.Update += (s, e) =>
            {
                currentCircleWidth = (float)e.Animation.AnimatedValue;
                currentCircleHeight = (float)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueAnimator.Start();
        }

        public void startTranslateBottomAnimation()
        {
            float translationYFrom = -(260 * parentWidth) / 700;
            float translationYTo = (360 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", translationYFrom, translationYTo);
            translationY.SetDuration(650);
            translationY.Start();
        }

        public void startScaleDisappear()
        {
            float maxScaleSize = (250 * parentWidth) / 700;
            ValueAnimator valueScaleWidthAnimator = ValueAnimator.OfFloat(circleRadius, maxScaleSize);
            valueScaleWidthAnimator.SetDuration(260);
            valueScaleWidthAnimator.StartDelay = 430;
            valueScaleWidthAnimator.Update += (s, e) =>
            {
                currentCircleWidth = (float)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueScaleWidthAnimator.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.MAIN_CIRCLE_SCALED_DISAPPEAR);
                currentCircleWidth = circleRadius + (strokeWidth / 2);
                currentCircleHeight = circleRadius + (strokeWidth / 2);
            };
            valueScaleWidthAnimator.Start();

            ValueAnimator valueScaleHeightAnimator = ValueAnimator.OfFloat(circleRadius, circleRadius / 2);
            valueScaleHeightAnimator.SetDuration(260);
            valueScaleHeightAnimator.StartDelay = 430;
            valueScaleHeightAnimator.Update += (s, e) =>
              {
                  currentCircleHeight = (float)e.Animation.AnimatedValue;
                  Invalidate();
              };
            valueScaleHeightAnimator.Start();
        }

        public void startTranslateCenterAnimation()
        {
            float translationYFrom = -(260 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", translationYFrom, 0);
            translationY.SetDuration(650);
            translationY.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.MAIN_CIRCLE_TRANSLATED_CENTER);
            };
            translationY.Start();
        }
    }
}