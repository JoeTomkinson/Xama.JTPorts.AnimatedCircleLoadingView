using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using Xama.JTPorts.AnimatedCircleLoadingView.component;
using Xama.JTPorts.AnimatedCircleLoadingView.component.finish;
using Xama.JTPorts.AnimatedCircleLoadingView.interfaces;

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
        private NormalTextView normalTextView;
        private animator.ViewAnimator viewAnimator;
        private IAnimationListener animationListener;
        private bool startAnimationIndeterminate;
        private bool startAnimationDeterminate;
        private bool stopAnimationOk;
        private bool stopAnimationFailure;

        public string TitleText { get; set; }
        public int? TitleTextSize { get; set; }
        public int MainColor { get; set; }
        public int SecondaryColor { get; set; }
        public int CheckMarkTintColor { get; set; }
        public int FailureMarkTintColor { get; set; }
        public int TextColor { get; set; }

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
                Context.ObtainStyledAttributes(attrs, Resource.Styleable.AnimatedCircleLoadingView);
            MainColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_mainColor,
                Color.ParseColor(DEFAULT_HEX_MAIN_COLOR));
            SecondaryColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_secondaryColor,
                Color.ParseColor(DEFAULT_HEX_SECONDARY_COLOR));
            CheckMarkTintColor =
                attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_checkMarkTintColor,
                    Color.ParseColor(DEFAULT_HEX_TINT_COLOR));
            FailureMarkTintColor =
                attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_failureMarkTintColor,
                    Color.ParseColor(DEFAULT_HEX_TINT_COLOR));
            TextColor = attributes.GetColor(Resource.Styleable.AnimatedCircleLoadingView_animCircleLoadingView_textColor,
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
                    if (!string.IsNullOrEmpty(TitleText))
                    {
                        normalTextView = new NormalTextView(context, Width, TextColor);
                        normalTextView.SetNormalText(TitleText);
                        if (TitleTextSize == null)
                        {
                            normalTextView.TrySetOptimalTextSize(Width);
                        }
                        else
                        {
                            normalTextView.SetTextSize((int)TitleTextSize);
                        }
                       
                        AddView(normalTextView);
                    }

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
            initialCenterCircleView = new InitialCenterCircleView(context, width, MainColor, SecondaryColor);
            rightCircleView = new RightCircleView(context, width, MainColor, SecondaryColor);
            sideArcsView = new SideArcsView(context, width, MainColor, SecondaryColor);
            topCircleBorderView = new TopCircleBorderView(context, width, MainColor, SecondaryColor);
            mainCircleView = new MainCircleView(context, width, MainColor, SecondaryColor);
            finishedOkView = new FinishedOKView(context, width, MainColor, SecondaryColor, CheckMarkTintColor);
            finishedFailureView = new FinishedFailureView(context, width, MainColor, SecondaryColor, FailureMarkTintColor);
            percentIndicatorView = new PercentIndicatorView(context, width, TextColor);
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
