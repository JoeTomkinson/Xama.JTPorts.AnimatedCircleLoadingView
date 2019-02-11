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
            Init();
        }

        private void Init()
        {
            rightMargin = (150 * parentWidth / 700);
            bottomMargin = (50 * parentWidth / 700);
            InitPaint();
        }

        private void InitPaint()
        {
            paint = new Paint();
            paint.SetStyle(Paint.Style.Fill);
            paint.SetColorFilter(secondaryColor);
            paint.AntiAlias = true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawCircle(canvas);
        }

        public void DrawCircle(Canvas canvas)
        {
            canvas.DrawCircle(Width - rightMargin, parentCenter - bottomMargin, circleRadius, paint);
        }

        public void StartSecondaryCircleAnimation()
        {
            int bottomMovementAddition = (260 * parentWidth) / 700;
            TranslateAnimation translateAnimation = new TranslateAnimation(GetX(), GetX(), GetY(), GetY() + bottomMovementAddition)
            {
                StartOffset = 200,
                Duration = 1000
            };

            AlphaAnimation alphaAnimation = new AlphaAnimation(1, 0)
            {
                StartOffset = 1300,
                Duration = 200
            };

            AnimationSet animationSet = new AnimationSet(true)
            {
                FillAfter = true
            };

            animationSet.AddAnimation(translateAnimation);
            animationSet.AddAnimation(alphaAnimation);
            animationSet.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.SecondaryCircleFinished);
            };

            StartAnimation(animationSet);
        }
    }
}