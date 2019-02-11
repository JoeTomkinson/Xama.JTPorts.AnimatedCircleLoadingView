using Android.Content;
using Android.Views;
using Xama.JTPorts.AnimatedCircleLoadingView.animator;
using Xama.JTPorts.AnimatedCircleLoadingView.component.interfaces;
using Xama.JTPorts.AnimatedCircleLoadingView.exception;

namespace Xama.JTPorts.AnimatedCircleLoadingView.component
{
    class ComponentViewAnimation : View
    {
        protected int parentWidth;
        protected int mainColor;
        protected int secondaryColor;
        protected float parentCenter;
        protected float circleRadius;
        protected int strokeWidth;
        private IStateListener stateListener;

        public ComponentViewAnimation(Context context, int parentWidth, int mainColor, int secondaryColor) : base(context)
        {
            this.parentWidth = parentWidth;
            this.mainColor = mainColor;
            this.secondaryColor = secondaryColor;
            Init();
        }

        private void Init()
        {
            HideView();
            circleRadius = parentWidth / 10;
            parentCenter = parentWidth / 2;
            strokeWidth = (12 * parentWidth) / 700;
        }

        public void HideView()
        {
            Visibility = ViewStates.Gone;
        }

        public void ShowView()
        {
            Visibility = ViewStates.Visible;
        }

        public void SetState(AnimationState state)
        {
            if (stateListener != null)
            {
                stateListener.OnStateChanged(state);
            }
            else
            {
                throw new NullStateListenerException();
            }
        }

        public void SetStateListener(IStateListener stateListener)
        {
            this.stateListener = stateListener;
        }
    }
}