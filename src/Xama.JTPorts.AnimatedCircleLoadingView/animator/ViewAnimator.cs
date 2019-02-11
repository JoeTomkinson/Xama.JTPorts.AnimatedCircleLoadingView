using Xama.JTPorts.AnimatedCircleLoadingView.component;
using Xama.JTPorts.AnimatedCircleLoadingView.component.finish;
using Xama.JTPorts.AnimatedCircleLoadingView.component.interfaces;
using Xama.JTPorts.AnimatedCircleLoadingView.interfaces;

namespace Xama.JTPorts.AnimatedCircleLoadingView.animator
{
    class ViewAnimator : IStateListener
    {
        private InitialCenterCircleView initialCenterCircleView;
        private RightCircleView rightCircleView;
        private SideArcsView sideArcsView;
        private TopCircleBorderView topCircleBorderView;
        private MainCircleView mainCircleView;
        private FinishedOKView finishedOkView;
        private FinishedFailureView finishedFailureView;
        private PercentIndicatorView percentIndicatorView;
        private AnimationState finishedState;
        private bool animatorReset;
        private IAnimationListener animationListener;

        public void SetComponentViewAnimations(InitialCenterCircleView initialCenterCircleView,
      RightCircleView rightCircleView, SideArcsView sideArcsView,
      TopCircleBorderView topCircleBorderView, MainCircleView mainCircleView,
      FinishedOKView finishedOkCircleView, FinishedFailureView finishedFailureView,
      PercentIndicatorView percentIndicatorView)
        {
            this.initialCenterCircleView = initialCenterCircleView;
            this.rightCircleView = rightCircleView;
            this.sideArcsView = sideArcsView;
            this.topCircleBorderView = topCircleBorderView;
            this.mainCircleView = mainCircleView;
            this.finishedOkView = finishedOkCircleView;
            this.finishedFailureView = finishedFailureView;
            this.percentIndicatorView = percentIndicatorView;
            InitListeners();
        }

        private void InitListeners()
        {
            initialCenterCircleView.SetStateListener(this);
            rightCircleView.SetStateListener(this);
            sideArcsView.SetStateListener(this);
            topCircleBorderView.SetStateListener(this);
            mainCircleView.SetStateListener(this);
            finishedOkView.SetStateListener(this);
            finishedFailureView.SetStateListener(this);
        }

        public void StartAnimator()
        {
            finishedState =  AnimationState.NullAnimation;
            initialCenterCircleView.ShowView();
            initialCenterCircleView.StartTranslateTopAnimation();
            initialCenterCircleView.StartScaleAnimation();
            rightCircleView.ShowView();
            rightCircleView.StartSecondaryCircleAnimation();
        }

        public void ResetAnimator()
        {
            initialCenterCircleView.HideView();
            rightCircleView.HideView();
            sideArcsView.HideView();
            topCircleBorderView.HideView();
            mainCircleView.HideView();
            finishedOkView.HideView();
            finishedFailureView.HideView();
            animatorReset = true;
            StartAnimator();
        }

        public void FinishOk()
        {
            finishedState = AnimationState.FinishedOk;
        }

        public void FinishFailure()
        {
            finishedState = AnimationState.FinishedFailure;
        }

        public void SetAnimationListener(IAnimationListener animationListener)
        {
            this.animationListener = animationListener;
        }
        
        public void OnStateChanged(AnimationState state)
        {
            if (animatorReset)
            {
                animatorReset = false;
            }
            else
            {
                switch (state)
                {
                    case AnimationState.MainCircleTranslatedTop:
                        OnMainCircleTranslatedTop();
                        break;
                    case AnimationState.MainCircleScaledDisappear:
                        OnMainCircleScaledDisappear();
                        break;
                    case AnimationState.MainCircleFilledTop:
                        OnMainCircleFilledTop();
                        break;
                    case AnimationState.SideArcsResizedTops:
                        OnSideArcsResizedTop();
                        break;
                    case AnimationState.MainCircleDrawnTop:
                        OnMainCircleDrawnTop();
                        break;
                    case AnimationState.FinishedOk:
                        OnFinished(state);
                        break;
                    case AnimationState.FinishedFailure:
                        OnFinished(state);
                        break;
                    case AnimationState.MainCircleTranslatedCenter:
                        OnMainCircleTranslatedCenter();
                        break;
                    case AnimationState.AnimationEnd:
                        OnAnimationEnd();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnMainCircleTranslatedTop()
        {
            initialCenterCircleView.StartTranslateBottomAnimation();
            initialCenterCircleView.StartScaleDisappear();
        }

        private void OnMainCircleScaledDisappear()
        {
            initialCenterCircleView.HideView();
            sideArcsView.ShowView();
            sideArcsView.StartRotateAnimation();
            sideArcsView.StartResizeDownAnimation();
        }

        private void OnSideArcsResizedTop()
        {
            topCircleBorderView.ShowView();
            topCircleBorderView.StartDrawCircleAnimation();
            sideArcsView.HideView();
        }

        private void OnMainCircleDrawnTop()
        {
            mainCircleView.ShowView();
            mainCircleView.StartFillCircleAnimation();
        }

        private void OnMainCircleFilledTop()
        {
            if (IsAnimationFinished)
            {
                OnStateChanged(finishedState);
                percentIndicatorView.StartAlphaAnimation();
            }
            else
            {
                topCircleBorderView.HideView();
                mainCircleView.HideView();
                initialCenterCircleView.ShowView();
                initialCenterCircleView.StartTranslateBottomAnimation();
                initialCenterCircleView.StartScaleDisappear();
            }
        }

        private bool IsAnimationFinished => finishedState != AnimationState.NullAnimation;

        private void OnFinished(AnimationState state)
        {
            topCircleBorderView.HideView();
            mainCircleView.HideView();
            finishedState = state;
            initialCenterCircleView.ShowView();
            initialCenterCircleView.StartTranslateCenterAnimation();
        }

        private void OnMainCircleTranslatedCenter()
        {
            if (finishedState == AnimationState.FinishedOk)
            {
                finishedOkView.ShowView();
                finishedOkView.StartScaleAnimation();
            }
            else
            {
                finishedFailureView.ShowView();
                finishedFailureView.StartScaleAnimation();
            }
            initialCenterCircleView.HideView();
        }

        private void OnAnimationEnd()
        {
            if (animationListener != null)
            {
                bool success = finishedState == AnimationState.FinishedOk;
                animationListener.OnAnimationEnd(success);
            }
        }
    }
}