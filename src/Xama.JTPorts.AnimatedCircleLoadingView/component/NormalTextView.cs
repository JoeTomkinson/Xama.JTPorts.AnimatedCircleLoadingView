using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class NormalTextView : TextView
    {
        private readonly int parentWidth;
        private readonly int textColor;

        public NormalTextView(Context context, int parentWidth, int textColor) : base(context)
        {
            this.parentWidth = parentWidth;
            this.textColor = textColor;
            Init();
        }

        private void Init()
        {
            int textSize = (35 * parentWidth) / 700;
            SetTextSize(Android.Util.ComplexUnitType.Dip, textSize);
            Color c = new Color(ContextCompat.GetColor(Context, this.textColor));
            SetTextColor(c);
            Gravity = GravityFlags.Center;
            Alpha = 0.8f;
        }

        public void SetNormalText(string text) => Text = text;

        public void SetTextSize(int textSize)
        {
            SetTextSize(Android.Util.ComplexUnitType.Dip, textSize);
        }

        public void TrySetOptimalTextSize(int parentWidth)
        {
            int textSize = (35 * (parentWidth)) / 700;
            SetTextSize(Android.Util.ComplexUnitType.Dip, textSize);
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