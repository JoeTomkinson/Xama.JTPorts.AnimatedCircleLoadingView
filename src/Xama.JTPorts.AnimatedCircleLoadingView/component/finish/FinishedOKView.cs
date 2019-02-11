using Android.Content;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component.finish
{
    class FinishedOKView : FinishedView
    {
        public FinishedOKView(Context context, int parentWidth, int mainColor, int secondaryColor, int tintColor) : base(context, parentWidth, mainColor, secondaryColor, tintColor)
        {
        }

        protected override int getCircleColor()
        {
            return Resource.Drawable.ic_checked_mark;
        }

        protected override int getDrawable()
        {
            return tintColor;
        }

        protected override int getDrawableTintColor()
        {
            return mainColor;
        }
    }
}