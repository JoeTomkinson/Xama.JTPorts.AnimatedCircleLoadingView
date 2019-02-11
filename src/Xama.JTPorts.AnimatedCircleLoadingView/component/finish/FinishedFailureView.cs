using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component.finish
{
    class FinishedFailureView : FinishedView
    {
        public FinishedFailureView(Context context, int parentWidth, int mainColor, int secondaryColor, int tintColor) : base(context, parentWidth, mainColor, secondaryColor, tintColor)
        {
        }

        protected override int getDrawable()
        {
            return Resource.Drawable.ic_failure_mark;
        }

        protected override int getDrawableTintColor()
        {
            return tintColor;
        }

        protected override int getCircleColor()
        {
            return secondaryColor;
        }
    }
}