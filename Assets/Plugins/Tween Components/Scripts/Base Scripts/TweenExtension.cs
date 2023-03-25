using DG.Tweening;

namespace TweenComponents.Base
{
    public static class TweenExtension
    {
        public static Tween ApplyBaseSettingsFrom(this Tween tween, TweenBase settings)
        {
            if (tween == null) return null;

            tween.onComplete += settings.OnExecutionCompleted;

            return tween.SetDelay(settings.Delay).SetLoops(settings.LoopCount).SetUpdate(settings.IgnoreTimeScale).SetEase(settings.EaseType);
        }
    }
}

