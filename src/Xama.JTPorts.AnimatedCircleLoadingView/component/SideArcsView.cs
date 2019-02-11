using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Views.Animations;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class SideArcsView : ComponentViewAnimation
    {
        private static int MIN_RESIZE_ANGLE = 8;
        private static int MAX_RESIZE_ANGLE = 45;
        private static int INITIAL_LEFT_ARC_START_ANGLE = 100;
        private static int INITIAL_RIGHT_ARC_START_ANGLE = 80;
        private static int MIN_START_ANGLE = 0;
        private static int MAX_START_ANGLE = 165;
        private int startLeftArcAngle = INITIAL_LEFT_ARC_START_ANGLE;
        private int startRightArcAngle = INITIAL_RIGHT_ARC_START_ANGLE;
        private Paint paint;
        private RectF oval;
        private int arcAngle;

        public SideArcsView(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context, parentWidth, mainColor, secondaryColor)
        {
            init();
        }

        private void init()
        {
            initPaint();
            arcAngle = MAX_RESIZE_ANGLE;
            initOval();
        }

        private void initPaint()
        {
            paint = new Paint();
            paint.SetColorFilter(mainColor);
            paint.StrokeWidth = (strokeWidth);
            paint.SetStyle(Paint.Style.Stroke);
            paint.AntiAlias = (true);
        }

        private void initOval()
        {
            float padding = paint.StrokeWidth / 2;
            oval = new RectF();
            oval.Set(padding, padding, parentWidth - padding, parentWidth - padding);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            drawArcs(canvas);
        }

        private void drawArcs(Canvas canvas)
        {
            canvas.DrawArc(oval, startLeftArcAngle, arcAngle, false, paint);
            canvas.DrawArc(oval, startRightArcAngle, -arcAngle, false, paint);
        }

        public void startRotateAnimation()
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

        public void startResizeDownAnimation()
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
                setState(AnimationState.SIDE_ARCS_RESIZED_TOP);
            };
            valueAnimator.Start();
        }
    }
}