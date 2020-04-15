using Android.Animation;
using Android.Content;
using Android.Graphics;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component.finish
{
    abstract class FinishedView : ComponentViewAnimation
    {
        private static int minimumImageSize = 1;
        protected int tintColor;
        private int maxImageSize;
        private int circleMaxRadius;
        private Bitmap originalFinishedBitmap;
        private float currentCircleRadius;
        private int imageSize;

        public FinishedView(Context context, int parentWidth, int mainColor, int secondaryColor, int tintColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            this.tintColor = tintColor;
            Init();
        }

        private void Init()
        {
            maxImageSize = (140 * parentWidth) / 700;
            circleMaxRadius = (140 * parentWidth) / 700;
            currentCircleRadius = circleRadius;
            imageSize = minimumImageSize;
            originalFinishedBitmap = BitmapFactory.DecodeResource(Resources, Drawable);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            DrawCircle(canvas);
            DrawCheckedMark(canvas);
        }

        private void DrawCheckedMark(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetColorFilter(new LightingColorFilter(DrawableTintColor, 0));

            Bitmap bitmap = Bitmap.CreateScaledBitmap(originalFinishedBitmap, imageSize, imageSize, true);
            canvas.DrawBitmap(bitmap, parentCenter - bitmap.Width / 2,
                parentCenter - bitmap.Height / 2, paint);
        }

        public void DrawCircle(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = Resources.GetColor(CircleColor, null);
            paint.AntiAlias = (true);
            canvas.DrawCircle(parentCenter, parentCenter, currentCircleRadius, paint);
        }

        public void StartScaleAnimation()
        {
            StartScaleCircleAnimation();
            StartScaleImageAnimation();
        }

        private void StartScaleCircleAnimation()
        {
            ValueAnimator valueCircleAnimator =
                ValueAnimator.OfFloat(circleRadius + strokeWidth / 2, circleMaxRadius);
            valueCircleAnimator.SetDuration(1000);
            valueCircleAnimator.Update += (s, e) =>
            {
                currentCircleRadius = (float)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueCircleAnimator.Start();
        }

        private void StartScaleImageAnimation()
        {
            ValueAnimator valueImageAnimator = ValueAnimator.OfInt(minimumImageSize, maxImageSize);
            valueImageAnimator.SetDuration(1000);
            valueImageAnimator.Update += (s, e) =>
            {
                imageSize = (int)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueImageAnimator.AnimationEnd += (s, e) =>
            {
                SetState(AnimationState.AnimationEnd);
            };
            valueImageAnimator.Start();
        }

        protected abstract int Drawable { get; }

        protected abstract int DrawableTintColor { get; }

        protected abstract int CircleColor { get; }
    }
}