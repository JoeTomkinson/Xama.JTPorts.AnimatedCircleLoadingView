using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using Xama.JTPorts.AnimatedCircleLoadingView.component;
using Xama.JTPorts.AnimatedCircleLoadingView.component.finish;
using IAnimationListener = Xama.JTPorts.AnimatedCircleLoadingView.interfaces.IAnimationListener;

namespace Xama.JTPorts.AnimatedCircleLoadingView
{
    public class AnimatedCircleLoadingView : FrameLayout
    {
        private const string DEFAULT_HEX_MAIN_COLOR = "#FF9A00";
        private const string DEFAULT_HEX_SECONDARY_COLOR = "#BDBDBD";
        private const string DEFAULT_HEX_TINT_COLOR = "#FFFFFF";
        private const string DEFAULT_HEX_TEXT_COLOR = "#FFFFFF";
        private readonly Context context;
        private InitialCenterCircleView initialCenterCircleView;
        private MainCircleView mainCircleView;
        private RightCircleView rightCircleView;
        private SideArcsView sideArcsView;
        private TopCircleBorderView topCircleBorderView;
        private FinishedOKView finishedOkView;
        private FinishedFailureView finishedFailureView;
        private PercentIndicatorView percentIndicatorView;
        private animator.ViewAnimator viewAnimator;
        private IAnimationListener animationListener;
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
            InitAttributes(attrs);
        }

        public AnimatedCircleLoadingView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            this.context = context;
            InitAttributes(attrs);
        }

        private void InitAttributes(IAttributeSet attrs)
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
            Init();
            StartAnimation();
        }

        private void StartAnimation()
        {
            if (Width != 0 && Height != 0)
            {
                if (startAnimationIndeterminate)
                {
                    viewAnimator.StartAnimator();
                    startAnimationIndeterminate = false;
                }
                if (startAnimationDeterminate)
                {
                    AddView(percentIndicatorView);
                    viewAnimator.StartAnimator();
                    startAnimationDeterminate = false;
                }
                if (stopAnimationOk)
                {
                    StopOk();
                }
                if (stopAnimationFailure)
                {
                    StopFailure();
                }
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        private void Init()
        {
            InitComponents();
            AddComponentsViews();
            InitAnimatorHelper();
        }

        private void InitComponents()
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

        private void AddComponentsViews()
        {
            AddView(initialCenterCircleView);
            AddView(rightCircleView);
            AddView(sideArcsView);
            AddView(topCircleBorderView);
            AddView(mainCircleView);
            AddView(finishedOkView);
            AddView(finishedFailureView);
        }

        private void InitAnimatorHelper()
        {
            viewAnimator = new animator.ViewAnimator();
            viewAnimator.SetAnimationListener(this.animationListener);
            viewAnimator.SetComponentViewAnimations(initialCenterCircleView, rightCircleView, sideArcsView,
                topCircleBorderView, mainCircleView, finishedOkView, finishedFailureView,
                percentIndicatorView);
        }

        public void StartIndeterminate()
        {
            startAnimationIndeterminate = true;
            StartAnimation();
        }

        public void StartDeterminate()
        {
            startAnimationDeterminate = true;
            StartAnimation();
        }

        public void SetPercent(int percent)
        {
            if (percentIndicatorView != null)
            {
                percentIndicatorView.SetPercent(percent);
                if (percent == 100)
                {
                    viewAnimator.FinishOk();
                }
            }
        }

        public void StopOk()
        {
            if (viewAnimator == null)
            {
                stopAnimationOk = true;
            }
            else
            {
                viewAnimator.FinishOk();
            }
        }

        public void StopFailure()
        {
            if (viewAnimator == null)
            {
                stopAnimationFailure = true;
            }
            else
            {
                viewAnimator.FinishFailure();
            }
        }

        public void ResetLoading()
        {
            if (viewAnimator != null)
            {
                viewAnimator.ResetAnimator();
            }
            SetPercent(0);
        }
    }
}
