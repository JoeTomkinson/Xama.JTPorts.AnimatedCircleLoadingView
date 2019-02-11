using Android.Animation;
using Android.Content;
using Android.Graphics;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component.finish
{
    abstract class FinishedView : ComponentViewAnimation
    {
        private static int MIN_IMAGE_SIZE = 1;
        protected int tintColor;
        private int maxImageSize;
        private int circleMaxRadius;
        private Bitmap originalFinishedBitmap;
        private float currentCircleRadius;
        private int imageSize;

        public FinishedView(Context context, int parentWidth, int mainColor, int secondaryColor, int tintColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            this.tintColor = tintColor;
            init();
        }

        private void init()
        {
            maxImageSize = (140 * parentWidth) / 700;
            circleMaxRadius = (140 * parentWidth) / 700;
            currentCircleRadius = circleRadius;
            imageSize = MIN_IMAGE_SIZE;
            originalFinishedBitmap = BitmapFactory.DecodeResource(Resources, getDrawable());
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            drawCircle(canvas);
            drawCheckedMark(canvas);
        }

        private void drawCheckedMark(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetColorFilter(new LightingColorFilter(getDrawableTintColor(), 0));

            Bitmap bitmap = Bitmap.CreateScaledBitmap(originalFinishedBitmap, imageSize, imageSize, true);
            canvas.DrawBitmap(bitmap, parentCenter - bitmap.Width / 2,
                parentCenter - bitmap.Height / 2, paint);
        }

        public void drawCircle(Canvas canvas)
        {
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = (getCircleColor());
            paint.AntiAlias = (true);
            canvas.DrawCircle(parentCenter, parentCenter, currentCircleRadius, paint);
        }

        public void startScaleAnimation()
        {
            startScaleCircleAnimation();
            startScaleImageAnimation();
        }

        private void startScaleCircleAnimation()
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

        private void startScaleImageAnimation()
        {
            ValueAnimator valueImageAnimator = ValueAnimator.OfInt(MIN_IMAGE_SIZE, maxImageSize);
            valueImageAnimator.SetDuration(1000);
            valueImageAnimator.Update += (s, e) =>
            {
                imageSize = (int)e.Animation.AnimatedValue;
                Invalidate();
            };
            valueImageAnimator.AnimationEnd += (s, e) =>
            {
                setState(AnimationState.ANIMATION_END);
            };
            valueImageAnimator.Start();
        }

        protected abstract int getDrawable();

        protected abstract int getDrawableTintColor();

        protected abstract int getCircleColor();
    }
}