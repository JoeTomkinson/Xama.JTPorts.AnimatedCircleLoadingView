using Android.Content;
using Android.Graphics;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component.finish
{
    class FinishedOKView : FinishedView
    {
        public FinishedOKView(Context context, int parentWidth, int mainColor, int secondaryColor, int tintColor) : base(context, parentWidth, mainColor, secondaryColor, tintColor)
        {
        }

        protected override int Drawable { get => Resource.Drawable.ic_checked_mark; }
        protected override int DrawableTintColor { get => tintColor; }
        protected override int CircleColor { get => mainColor; }
    }
}