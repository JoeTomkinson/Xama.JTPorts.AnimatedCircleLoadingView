using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class PercentIndicatorView : TextView
    {
        private int parentWidth;
        private int textColor = Color.White;

        public PercentIndicatorView(Context context, int parentWidth, int textColor) : base(context)
        {
            this.parentWidth = parentWidth;
            this.textColor = textColor;
            init();
        }

        private void init()
        {
            int textSize = (35 * parentWidth) / 700;
            SetTextSize(Android.Util.ComplexUnitType.Pt, textSize);
            SetTextColor(this.textColor);
            Gravity = GravityFlags.Center;
            Alpha = 0.8f;
        }

        public void setPercent(int percent)
        {
            Text = percent.ToString() + "%";
        }

        public void startAlphaAnimation()
        {
            AlphaAnimation alphaAnimation = new AlphaAnimation(1, 0);
            alphaAnimation.Duration = 700;
            alphaAnimation.FillAfter = true;
            StartAnimation(alphaAnimation);
        }
    }
}