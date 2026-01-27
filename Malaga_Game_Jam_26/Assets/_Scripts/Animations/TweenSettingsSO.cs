using UnityEngine;

namespace Bas.Pennings.UnityTools
{
    [CreateAssetMenu(fileName = "NewAnimationTweenSettings", menuName = "Ànimation/Animation Tween Settings")]
    public class TweenSettingsSO : ScriptableObject
    {
        public UIAnimationTypes AnimationType;
        public LeanTweenType EaseType;
        public AnimationCurve Curve;

        public float Duration = .3f;

        public bool Loop;
        public bool Pingpong;

        public TweenSettingsSO Copy()
        { 
            var settings = CreateInstance<TweenSettingsSO>();

            settings.AnimationType = AnimationType;
            settings.EaseType = EaseType;
            settings.Curve = Curve;
            settings.Duration = Duration;
            settings.Loop = Loop;
            settings.Pingpong = Pingpong;

            return settings;
        }

        public enum UIAnimationTypes
        {
            Fade,
            Move,
            Scale
        }
    }
}