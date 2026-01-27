using UnityEngine;

namespace Bas.Pennings.UnityTools
{
    public class UITweener : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToAnimate;
        [SerializeField] private TweenSettingsSO _OnEnableSettings;
        [SerializeField] private TweenSettingsSO _OnDisableSettings;
        [SerializeField] private bool _startPositionOffset;
        [SerializeField] private Vector3 _from = Vector3.zero;
        [SerializeField] private Vector3 _to = Vector3.one;
        [SerializeField] private float _delay;

        private async void OnEnable()
        {
            if (_OnEnableSettings != null)
                await LeanTweenWrapper.AnimateAsync(_objectToAnimate, _OnEnableSettings, _from, _to, _delay, _startPositionOffset);
        }

        private async void OnDisable()
        {
            if (_OnDisableSettings != null)
                await LeanTweenWrapper.AnimateAsync(_objectToAnimate, _OnDisableSettings, _to, _from, _delay, _startPositionOffset);
        }
    }
}