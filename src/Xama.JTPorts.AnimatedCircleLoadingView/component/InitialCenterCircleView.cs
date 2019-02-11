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
            Init();
        }

        private void Init()
        {
            InitOval();
            InitPaint();
        }

        private void InitPaint()
        {
            paint = new Paint();
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = Color.ParseColor(mainColor);
            paint.AntiAlias = true;
        }

        private void InitOval()
        {
            oval = new RectF();
            minRadius = (15 * parentWidth) / 700;
            currentCircleWidth = minRadius;
            currentCircleHeight = minRadius;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawCircle(canvas);
        }

        public void DrawCircle(Canvas canvas)
        {
            RectF oval = new RectF();
            oval.Set(parentCenter - currentCircleWidth, parentCenter - currentCircleHeight,
                parentCenter + currentCircleWidth, parentCenter + currentCircleHeight);
            canvas.DrawOval(oval, paint);
        }

        public void StartTranslateTopAnimation()
        {
            float translationYTo = -(255 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", 0, translationYTo);
            translationY.SetDuration(1100);
            translationY.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.MainCircleTranslatedTop);
            };
            translationY.Start();
        }

        public void StartScaleAnimation()
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

        public void StartTranslateBottomAnimation()
        {
            float translationYFrom = -(260 * parentWidth) / 700;
            float translationYTo = (360 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", translationYFrom, translationYTo);
            translationY.SetDuration(650);
            translationY.Start();
        }

        public void StartScaleDisappear()
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
                SetState(AnimationState.MainCircleScaledDisappear);
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

        public void StartTranslateCenterAnimation()
        {
            float translationYFrom = -(260 * parentWidth) / 700;
            ObjectAnimator translationY = ObjectAnimator.OfFloat(this, "translationY", translationYFrom, 0);
            translationY.SetDuration(650);
            translationY.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.MainCircleTranslatedCenter);
            };
            translationY.Start();
        }
    }
}