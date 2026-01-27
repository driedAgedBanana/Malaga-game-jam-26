using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bas.Pennings.UnityTools
{
    public class ModalWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [Space]

        [Header("Header")]
        [SerializeField] private GameObject _headerArea;
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Content")]
        [SerializeField] private GameObject _horizontalLayoutArea;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _iconText;
        [Space]
        [SerializeField] private GameObject _verticalLayoutArea;
        [SerializeField] private Image _heroImage;
        [SerializeField] private TextMeshProUGUI _heroText;

        [Header("Footer")]
        [SerializeField] private RectTransform _footerArea;
        [SerializeField] private ModalButtonUI _confirmBtnBinding;
        [SerializeField] private ModalButtonUI _declineBtnBinding;
        [SerializeField] private GameObject _alternateBtnPrefab;

        private readonly List<GameObject> alternateButtons = new();

        private void OnValidate()
        {
            if (_alternateBtnPrefab != null && _alternateBtnPrefab.GetComponent<AlternateModalButtonUI>() == null)
            {
                Debug.LogWarning($"Provided prefab does not contain a {typeof(AlternateModalButtonUI)} component! Assigning the field with null.", this);
                _alternateBtnPrefab = null;
            }
        }

        public void Close() => _root.SetActive(false);

        public void Setup(ModalWindowData data)
        {
            Clear();
            ClearAlternateButtons();
            SetupHeader(data.Content.Title);
            SetupContent(data.Layout, data.Content);
            SetupButtons(data.ConfirmBtnData, data.DeclineBtnData, data.AlternateButtonsData);
            _root.SetActive(true);
        }

        private void Clear()
        {
            _root.SetActive(false);
            _headerArea.SetActive(false);
            _horizontalLayoutArea.SetActive(false);
            _verticalLayoutArea.SetActive(false);
            _footerArea.gameObject.SetActive(false);
            _iconImage.gameObject.SetActive(false);
            _heroImage.gameObject.SetActive(false);
            _declineBtnBinding.Root.SetActive(false);
        }

        private void SetupHeader(string title)
        {
            _titleText.text = title;
            _headerArea.SetActive(!string.IsNullOrEmpty(title));
        }

        private void SetupContent(ModalWindowContentLayout layout, ModalWindowContent content)
        {
            bool useHorizontal = layout == ModalWindowContentLayout.Horizontal;

            if (useHorizontal)
            {
                _iconText.text = content.Message;
                _iconImage.sprite = content.Sprite;
                _iconImage.gameObject.SetActive(content.Sprite != null);
            }
            else
            {
                _heroText.text = content.Message;
                _heroImage.sprite = content.Sprite;
                _heroImage.gameObject.SetActive(content.Sprite != null);
            }

            _horizontalLayoutArea.SetActive(useHorizontal);
            _verticalLayoutArea.SetActive(!useHorizontal);
        }

        private void SetupButtons(ModalButtonData confirmBtnData, ModalButtonData cancelBtnData, List<ModalButtonData> altBtnsData)
        {
            _footerArea.gameObject.SetActive(true);

            SetupSingleButton(_confirmBtnBinding, confirmBtnData);

            if (cancelBtnData != null)
                SetupSingleButton(_declineBtnBinding, cancelBtnData);

            if (altBtnsData == null)
                return;

            foreach (var altData in altBtnsData)
            {
                var button = Instantiate(_alternateBtnPrefab, _footerArea);
                var refComp = button.GetComponent<AlternateModalButtonUI>();

                refComp.ButtonLabel.text = altData.LabelText;
                refComp.Button.onClick.RemoveAllListeners();
                refComp.Button.onClick.AddListener(() => altData.CallBack?.Invoke());

                alternateButtons.Add(button);
            }
        }

        private void SetupSingleButton(ModalButtonUI binding, ModalButtonData data)
        {
            binding.ButtonLabel.text = data.LabelText;
            binding.Button.onClick.RemoveAllListeners();
            binding.Root.SetActive(data.IsValid);

            if (data.IsValid)
                binding.Button.onClick.AddListener(() => data.CallBack?.Invoke());
        }

        private void ClearAlternateButtons()
        {
            foreach (GameObject btn in alternateButtons)
                Destroy(btn);

            alternateButtons.Clear();
        }

        [Serializable]
        private class ModalButtonUI
        {
            public GameObject Root;
            public Button Button;
            public TextMeshProUGUI ButtonLabel;
        }
    }
}