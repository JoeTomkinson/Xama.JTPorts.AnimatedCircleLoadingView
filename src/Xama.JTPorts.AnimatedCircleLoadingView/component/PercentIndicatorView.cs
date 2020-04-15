using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.Core.Content;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class PercentIndicatorView : TextView
    {
        private readonly int parentWidth;
        private readonly int textColor;

        public PercentIndicatorView(Context context, int parentWidth, int textColor) : base(context)
        {
            this.parentWidth = parentWidth;
            this.textColor = textColor;
            Init();
        }

        private void Init()
        {
            int textSize = (35 * parentWidth) / 700;
            SetTextSize(Android.Util.ComplexUnitType.Pt, textSize);
            Color c = new Color(ContextCompat.GetColor(Context, this.textColor));
            SetTextColor(c);
            Gravity = GravityFlags.Center;
            Alpha = 0.8f;
        }

        public void SetPercent(int percent)
        {
            Text = percent.ToString() + "%";
        }

        public void SetNormalText(string text)
        {
            Text = text;
        }

        public void StartAlphaAnimation()
        {
            AlphaAnimation alphaAnimation = new AlphaAnimation(1, 0)
            {
                Duration = 700,
                FillAfter = true
            };
            StartAnimation(alphaAnimation);
        }
    }
}