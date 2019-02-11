using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using Xama.JTPorts.AnimatedCircleLoadingView.component;
using Xama.JTPorts.AnimatedCircleLoadingView.component.finish;
using Xama.JTPorts.AnimatedCircleLoadingView.interfaces;
using static Android.Views.Animations.Animation;
using IAnimationListener = Xama.JTPorts.AnimatedCircleLoadingView.interfaces.IAnimationListener;

namespace Xama.JTPorts.AnimatedCircleLoadingView
{
    public class AnimatedCircleLoadingView : FrameLayout, IAnimationListener
    {
        private static string DEFAULT_HEX_MAIN_COLOR = "#FF9A00";
        private static string DEFAULT_HEX_SECONDARY_COLOR = "#BDBDBD";
        private static string DEFAULT_HEX_TINT_COLOR = "#FFFFFF";
        private static string DEFAULT_HEX_TEXT_COLOR = "#FFFFFF";
        private Context context;
        private InitialCenterCircleView initialCenterCircleView;
        private MainCircleView mainCircleView;
        private RightCircleView rightCircleView;
        private SideArcsView sideArcsView;
        private TopCircleBorderView topCircleBorderView;
        private FinishedOKView finishedOkView;
        private FinishedFailureView finishedFailureView;
        private PercentIndicatorView percentIndicatorView;
        private animator.ViewAnimator viewAnimator;
        private bool startAnimationIndeterminate;
        private bool startAnimationDeterminate;
        private bool stopAnimationOk;
        private bool stopAnimationFailure;
        private int mainColor;
        private int secondaryColor;
        private int checkMarkTintColor;
        private int failureMarkTintColor;
        private int textColor;

        public AnimatedCircleLoadingView(Context context) : base(context)
        {
            this.context = context;
        }

        public AnimatedCircleLoadingView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.context = context;
            initAttributes(attrs);
        }

        public AnimatedCircleLoadingView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            this.context = context;
            initAttributes(attrs);
        }

        private void initAttributes(IAttributeSet attrs)
        {
            TypedArray attributes =
                Context.ObtainStyledAttributes(attrs,Resource.Styleable.AnimatedCircleLoadingView);
            mainColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_mainColor,
                Color.ParseColor(DEFAULT_HEX_MAIN_COLOR));
            secondaryColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_secondaryColor,
                Color.ParseColor(DEFAULT_HEX_SECONDARY_COLOR));
            checkMarkTintColor =
                attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_checkMarkTintColor,
                    Color.ParseColor(DEFAULT_HEX_TINT_COLOR));
            failureMarkTintColor =
                attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_failureMarkTintColor,
                    Color.ParseColor(DEFAULT_HEX_TINT_COLOR));
            textColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_textColor,
                Color.ParseColor(DEFAULT_HEX_TEXT_COLOR));
            attributes.Recycle();
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            init();
            startAnimation();
        }

        private void startAnimation()
        {
            if (Width != 0 && Height != 0)
            {
                if (startAnimationIndeterminate)
                {
                    viewAnimator.startAnimator();
                    startAnimationIndeterminate = false;
                }
                if (startAnimationDeterminate)
                {
                    AddView(percentIndicatorView);
                    viewAnimator.startAnimator();
                    startAnimationDeterminate = false;
                }
                if (stopAnimationOk)
                {
                    stopOk();
                }
                if (stopAnimationFailure)
                {
                    stopFailure();
                }
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        private void init()
        {
            initComponents();
            addComponentsViews();
            initAnimatorHelper();
        }

        private void initComponents()
        {
            int width = Width;
            initialCenterCircleView =
                new InitialCenterCircleView(context, width, mainColor, secondaryColor);
            rightCircleView = new RightCircleView(context, width, mainColor, secondaryColor);
            sideArcsView = new SideArcsView(context, width, mainColor, secondaryColor);
            topCircleBorderView = new TopCircleBorderView(context, width, mainColor, secondaryColor);
            mainCircleView = new MainCircleView(context, width, mainColor, secondaryColor);
            finishedOkView =
                new FinishedOKView(context, width, mainColor, secondaryColor, checkMarkTintColor);
            finishedFailureView =
                new FinishedFailureView(context, width, mainColor, secondaryColor, failureMarkTintColor);
            percentIndicatorView = new PercentIndicatorView(context, width, textColor);
        }

        private void addComponentsViews()
        {
            AddView(initialCenterCircleView);
            AddView(rightCircleView);
            AddView(sideArcsView);
            AddView(topCircleBorderView);
            AddView(mainCircleView);
            AddView(finishedOkView);
            AddView(finishedFailureView);
        }

        private void initAnimatorHelper()
        {
            viewAnimator = new animator.ViewAnimator();
            viewAnimator.setAnimationListener(this);
            viewAnimator.setComponentViewAnimations(initialCenterCircleView, rightCircleView, sideArcsView,
                topCircleBorderView, mainCircleView, finishedOkView, finishedFailureView,
                percentIndicatorView);
        }

        public void startIndeterminate()
        {
            startAnimationIndeterminate = true;
            startAnimation();
        }

        public void startDeterminate()
        {
            startAnimationDeterminate = true;
            startAnimation();
        }

        public void setPercent(int percent)
        {
            if (percentIndicatorView != null)
            {
                percentIndicatorView.setPercent(percent);
                if (percent == 100)
                {
                    viewAnimator.finishOk();
                }
            }
        }

        public void stopOk()
        {
            if (viewAnimator == null)
            {
                stopAnimationOk = true;
            }
            else
            {
                viewAnimator.finishOk();
            }
        }

        public void stopFailure()
        {
            if (viewAnimator == null)
            {
                stopAnimationFailure = true;
            }
            else
            {
                viewAnimator.finishFailure();
            }
        }

        public void resetLoading()
        {
            if (viewAnimator != null)
            {
                viewAnimator.resetAnimator();
            }
            setPercent(0);
        }

        public void setAnimationListener(IAnimationListener animationListener)
        {
            this.animationListener = animationListener;
        }
    }
}
