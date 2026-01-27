using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static Bas.Pennings.UnityTools.TweenSettingsSO;

namespace Bas.Pennings.UnityTools
{
    public static class LeanTweenWrapper
    {
        public static Task FadeInAsync(GameObject target, TweenSettingsSO settings, float delay = 0,
            bool startTransparent = true, float? durationOverride = null, CancellationToken token = default)
            => RunAnimationAsync(UIAnimationTypes.Fade, target, settings, Vector3.zero, Vector3.one, delay, startTransparent, durationOverride, token);

        public static Task FadeOutAsync(GameObject target, TweenSettingsSO settings, float delay = 0,
            bool startTransparent = true, float? durationOverride = null, CancellationToken token = default)
            => RunAnimationAsync(UIAnimationTypes.Fade, target, settings, Vector3.one, Vector3.zero, delay, startTransparent, durationOverride, token);

        public static Task MoveAsync(GameObject target, TweenSettingsSO settings, Vector3 startPosition, Vector3 targetPosition,
            float delay = 0, bool applyStartPosition = true, float? durationOverride = null, CancellationToken token = default)
            => RunAnimationAsync(UIAnimationTypes.Move, target, settings, startPosition, targetPosition, delay, applyStartPosition, token: token);

        public static Task ScaleUpAsync(GameObject target, TweenSettingsSO settings, float delay = 0,
            bool applyStartScale = true, float? durationOverride = null, CancellationToken token = default)
            => RunAnimationAsync(UIAnimationTypes.Scale, target, settings, Vector3.zero, Vector3.one, delay, applyStartScale, durationOverride, token);

        public static Task ScaleDownAsync(GameObject target, TweenSettingsSO settings, float delay = 0, 
            bool applyStartScale = true, float? durationOverride = null, CancellationToken token = default)
            => RunAnimationAsync(UIAnimationTypes.Scale, target, settings, Vector3.one, Vector3.zero, delay, applyStartScale, durationOverride, token);

        public static async Task CrossFadeAsync(GameObject fadeOutTarget, GameObject fadeInTarget,
            TweenSettingsSO settings, float fadeOutDelay = 0f, float fadeInDelay = 0f, CancellationToken token = default)
        {
            try
            {
                await Task.WhenAll(
                    FadeOutAsync(fadeOutTarget, settings, fadeOutDelay, false, token: token),
                    FadeInAsync(fadeInTarget, settings, fadeInDelay, true, token: token));
            }
            catch (TaskCanceledException)
            {
                LeanTween.cancel(fadeOutTarget);
                LeanTween.cancel(fadeInTarget);
            }
        }

        public static Task AnimateAsync(GameObject target, TweenSettingsSO settings, Vector3 startValue, Vector3 targetValue, 
            float delay = 0, bool applyStartValue = true, float? durationOverride = null, CancellationToken token = default)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), "Target GameObject to animate cannot be null.");

            if (settings == null)
                throw new ArgumentNullException(nameof(settings), "TweenSettingsSO cannot be null.");

            if (delay < 0)
                throw new ArgumentOutOfRangeException(nameof(delay), "Delay cannot be negative.");

            if (durationOverride.HasValue && durationOverride.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(durationOverride), "Duration override cannot be negative.");

            var completionSource = new TaskCompletionSource<bool>();

            settings = settings.Copy();
            settings.Duration = durationOverride ?? settings.Duration;

            LTDescr tween = null;
            CancellationTokenRegistration registration = default;

            if (token.CanBeCanceled)
                registration = token.Register(() =>
                {
                    if (tween != null)
                    {
                        LeanTween.cancel(tween.uniqueId);
                        completionSource.TrySetCanceled();
                    }
                });  

            switch (settings.AnimationType)
            {
                case UIAnimationTypes.Fade:
                    var canvasGroup = target.GetComponent<CanvasGroup>();

                    if (canvasGroup == null)
                    {
                        Debug.LogError($"[LeanTweenWrapper] Missing component {typeof(CanvasGroup).Name} on {target.name}. Animation canceled.");
                        completionSource.TrySetCanceled();
                        return Task.CompletedTask;
                    }
                    else tween = Fade(canvasGroup);
                    break;

                case UIAnimationTypes.Move:
                    tween = MoveAbsolute();
                    break;

                case UIAnimationTypes.Scale:
                    tween = ScaleTarget();
                    break;
            }

            SetTweenProperties();
            return completionSource.Task;

            void SetTweenProperties()
            {
                tween.setDelay(delay);

                if (settings.EaseType == LeanTweenType.animationCurve)
                    tween.setEase(settings.Curve);
                else
                    tween.setEase(settings.EaseType);

                if (settings.Loop) tween.setLoopClamp(int.MaxValue);
                if (settings.Pingpong) tween.setLoopPingPong();

                tween.setOnComplete(() => 
                {
                    registration.Dispose();

                    if (!completionSource.Task.IsCompleted)
                        completionSource.TrySetResult(true);                    
                });
            }

            LTDescr Fade(CanvasGroup canvasGroup)
            {
                if (applyStartValue)
                    canvasGroup.alpha = startValue.x;

                return LeanTween.alphaCanvas(canvasGroup, targetValue.x, settings.Duration);
            }

            LTDescr MoveAbsolute()
            {
                if (target.TryGetComponent(out RectTransform rectTransform))
                {
                    if (applyStartValue)
                        rectTransform.anchoredPosition = startValue;

                    return LeanTween.move(rectTransform, targetValue, settings.Duration);
                }
                else
                {
                    if (applyStartValue)
                        target.transform.position = startValue;
                        
                    return LeanTween.move(target, targetValue, settings.Duration);
                }
            }

            LTDescr ScaleTarget()
            {
                if (applyStartValue)
                    target.transform.localScale = startValue;

                return LeanTween.scale(target, targetValue, settings.Duration);
            } 
        }

        private static Task RunAnimationAsync(UIAnimationTypes animationType, GameObject target, TweenSettingsSO settings, Vector3 startValue, Vector3 targetValue,
            float delay = 0, bool applyStartValue = true, float? durationOverride = null, CancellationToken token = default)
        {
            var modifiedSettings = settings.Copy();
            modifiedSettings.AnimationType = animationType;

            return AnimateAsync(target, modifiedSettings, startValue, targetValue, delay, applyStartValue, durationOverride, token);
        }
    }
}