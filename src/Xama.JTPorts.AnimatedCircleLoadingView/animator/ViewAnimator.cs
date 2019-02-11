using Xama.JTPorts.AnimatedCircleLoadingView.component;
using Xama.JTPorts.AnimatedCircleLoadingView.component.finish;
using Xama.JTPorts.AnimatedCircleLoadingView.component.interfaces;

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
        private AnimatedCircleLoadingView.AnimationListener animationListener;

        public void setComponentViewAnimations(InitialCenterCircleView initialCenterCircleView,
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
            initListeners();
        }

        private void initListeners()
        {
            initialCenterCircleView.setStateListener(this);
            rightCircleView.setStateListener(this);
            sideArcsView.setStateListener(this);
            topCircleBorderView.setStateListener(this);
            mainCircleView.setStateListener(this);
            finishedOkView.setStateListener(this);
            finishedFailureView.setStateListener(this);
        }

        public void startAnimator()
        {
            finishedState =  AnimationState.NONE;
            initialCenterCircleView.showView();
            initialCenterCircleView.startTranslateTopAnimation();
            initialCenterCircleView.startScaleAnimation();
            rightCircleView.showView();
            rightCircleView.startSecondaryCircleAnimation();
        }

        public void resetAnimator()
        {
            initialCenterCircleView.hideView();
            rightCircleView.hideView();
            sideArcsView.hideView();
            topCircleBorderView.hideView();
            mainCircleView.hideView();
            finishedOkView.hideView();
            finishedFailureView.hideView();
            animatorReset = true;
            startAnimator();
        }

        public void finishOk()
        {
            finishedState = AnimationState.FINISHED_OK;
        }

        public void finishFailure()
        {
            finishedState = AnimationState.FINISHED_FAILURE;
        }

        public void setAnimationListener(AnimatedCircleLoadingView.AnimationListener animationListener)
        {
            this.animationListener = animationListener;
        }
        
        public void onStateChanged(AnimationState state)
        {
            if (animatorReset)
            {
                animatorReset = false;
            }
            else
            {
                switch (state)
                {
                    case AnimationState.MAIN_CIRCLE_TRANSLATED_TOP:
                        onMainCircleTranslatedTop();
                        break;
                    case AnimationState.MAIN_CIRCLE_SCALED_DISAPPEAR:
                        onMainCircleScaledDisappear();
                        break;
                    case AnimationState.MAIN_CIRCLE_FILLED_TOP:
                        onMainCircleFilledTop();
                        break;
                    case AnimationState.SIDE_ARCS_RESIZED_TOP:
                        onSideArcsResizedTop();
                        break;
                    case AnimationState.MAIN_CIRCLE_DRAWN_TOP:
                        onMainCircleDrawnTop();
                        break;
                    case AnimationState.FINISHED_OK:
                        onFinished(state);
                        break;
                    case AnimationState.FINISHED_FAILURE:
                        onFinished(state);
                        break;
                    case AnimationState.MAIN_CIRCLE_TRANSLATED_CENTER:
                        onMainCircleTranslatedCenter();
                        break;
                    case AnimationState.ANIMATION_END:
                        onAnimationEnd();
                        break;
                    default:
                        break;
                }
            }
        }

        private void onMainCircleTranslatedTop()
        {
            initialCenterCircleView.startTranslateBottomAnimation();
            initialCenterCircleView.startScaleDisappear();
        }

        private void onMainCircleScaledDisappear()
        {
            initialCenterCircleView.hideView();
            sideArcsView.showView();
            sideArcsView.startRotateAnimation();
            sideArcsView.startResizeDownAnimation();
        }

        private void onSideArcsResizedTop()
        {
            topCircleBorderView.showView();
            topCircleBorderView.startDrawCircleAnimation();
            sideArcsView.hideView();
        }

        private void onMainCircleDrawnTop()
        {
            mainCircleView.showView();
            mainCircleView.startFillCircleAnimation();
        }

        private void onMainCircleFilledTop()
        {
            if (isAnimationFinished())
            {
                onStateChanged(finishedState);
                percentIndicatorView.startAlphaAnimation();
            }
            else
            {
                topCircleBorderView.hideView();
                mainCircleView.hideView();
                initialCenterCircleView.showView();
                initialCenterCircleView.startTranslateBottomAnimation();
                initialCenterCircleView.startScaleDisappear();
            }
        }

        private bool isAnimationFinished()
        {
            return finishedState !=  AnimationState.NONE;
        }

        private void onFinished(AnimationState state)
        {
            topCircleBorderView.hideView();
            mainCircleView.hideView();
            finishedState = state;
            initialCenterCircleView.showView();
            initialCenterCircleView.startTranslateCenterAnimation();
        }

        private void onMainCircleTranslatedCenter()
        {
            if (finishedState == AnimationState.FINISHED_OK)
            {
                finishedOkView.showView();
                finishedOkView.startScaleAnimation();
            }
            else
            {
                finishedFailureView.showView();
                finishedFailureView.startScaleAnimation();
            }
            initialCenterCircleView.hideView();
        }

        private void onAnimationEnd()
        {
            if (animationListener != null)
            {
                bool success = finishedState == AnimationState.FINISHED_OK;
                animationListener.onAnimationEnd(success);
            }
        }
    }
}