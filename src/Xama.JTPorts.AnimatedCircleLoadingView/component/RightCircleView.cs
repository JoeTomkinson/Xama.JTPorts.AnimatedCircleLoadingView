using Android.Content;
using Android.Graphics;
using Android.Views.Animations;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class RightCircleView : ComponentViewAnimation
    {
        private int rightMargin;
        private int bottomMargin;
        private Paint paint;

        public RightCircleView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            init();
        }

        private void init()
        {
            rightMargin = (150 * parentWidth / 700);
            bottomMargin = (50 * parentWidth / 700);
            initPaint();
        }

        private void initPaint()
        {
            paint = new Paint();
            paint.SetStyle(Paint.Style.Fill);
            paint.SetColorFilter(secondaryColor);
            paint.AntiAlias = true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            drawCircle(canvas);
        }

        public void drawCircle(Canvas canvas)
        {
            canvas.DrawCircle(Width - rightMargin, parentCenter - bottomMargin, circleRadius, paint);
        }

        public void startSecondaryCircleAnimation()
        {
            int bottomMovementAddition = (260 * parentWidth) / 700;
            TranslateAnimation translateAnimation =
                new TranslateAnimation(GetX(), GetX(), GetY(), GetY() + bottomMovementAddition);
            translateAnimation.StartOffset = 200;
            translateAnimation.Duration = 1000;

            AlphaAnimation alphaAnimation = new AlphaAnimation(1, 0);
            alphaAnimation.StartOffset = 1300;
            alphaAnimation.Duration = 200;

            AnimationSet animationSet = new AnimationSet(true);
            animationSet.AddAnimation(translateAnimation);
            animationSet.AddAnimation(alphaAnimation);
            animationSet.FillAfter = true;
            animationSet.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.SECONDARY_CIRCLE_FINISHED);
            };

            StartAnimation(animationSet);
        }
    }
}