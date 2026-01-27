using System;
using System.Threading.Tasks;
using Bas.Pennings.DevTools;
using UnityEngine;

namespace Bas.Pennings.UnityTools
{
    public class ModalWindowService : AbstractSingleton<ModalWindowService>
    {
        [Header("Tweening")]
        [SerializeField] private TweenSettingsSO _animationSettings;
        [SerializeField] private bool _startPositionOffset;
        [SerializeField] private Vector3 _from = Vector3.zero;
        [SerializeField] private Vector3 _to = Vector3.one;

        [Header("References")]
        [SerializeField] private ModalWindow _modalWindowPrefab;
        [SerializeField] private RectTransform _modalWindowParent;

        private bool isShowingModal = false;
        private ModalWindow activeWindow;

        public async Task ShowConfirmationAsync(string title, string message, Action onConfirm, Action onCancel)
        {
            await AwaitModalReadyAsync();

            var tcs = new TaskCompletionSource<bool>();

            ShowCustomWindow(new ModalWindowData
            {
                Content = new ModalWindowContent
                {
                    Title = title,
                    Message = message
                },
                Layout = ModalWindowContentLayout.Horizontal,
                ConfirmBtnData = new ModalButtonData("Confirm", () => {
                    tcs.TrySetResult(false);
                    onConfirm?.Invoke();
                }),
                DeclineBtnData = new ModalButtonData("Cancel", () => {
                    tcs.TrySetResult(false);
                    onCancel?.Invoke();
                })
            });

            await ShowActiveWindowAsync();
            await tcs.Task;

            await HideActiveWindowAsync();
            ResetModalFlag();
        }

        public async Task ShowNotificationAsync(string title, string message, Action onOk)
        {
            await AwaitModalReadyAsync();
            var tcs = new TaskCompletionSource<bool>();

            ShowCustomWindow(new ModalWindowData
            {
                Content = new ModalWindowContent
                {
                    Title = title,
                    Message = message
                },
                Layout = ModalWindowContentLayout.Horizontal,
                ConfirmBtnData = new ModalButtonData("Ok", () => tcs.TrySetResult(true))
            });

            await ShowActiveWindowAsync();
            await tcs.Task;

            await HideActiveWindowAsync();
            onOk?.Invoke();
            ResetModalFlag();
        }

        public async Task ShowDialogueAsync(string title, string[] messages, Sprite[] images)
        {
            if (messages.Length == 0 || messages.Length != images.Length)
                throw new ArgumentException("Messages and images must be non-empty and of equal length.");

            await AwaitModalReadyAsync();

            for (int i = 0; i < messages.Length - 1; i++) 
                await ShowDialogueMessageAsync(title, messages[i], images[i], "Next");

            var finalTcs = new TaskCompletionSource<bool>();

            ShowCustomWindow(new ModalWindowData
            {
                Content = new ModalWindowContent
                {
                    Title = title,
                    Message = messages[^1],
                    Sprite = images[^1]
                },
                Layout = ModalWindowContentLayout.Vertical,
                ConfirmBtnData = new ModalButtonData("Got it", () => finalTcs.TrySetResult(true)),
                DeclineBtnData = new ModalButtonData("Read again", () => finalTcs.TrySetResult(false))
            });

            await ShowActiveWindowAsync();

            bool restart = !await finalTcs.Task;
            if (restart)
            {
                ResetModalFlag();
                await ShowDialogueAsync(title, messages, images);
                return;
            }

            await HideActiveWindowAsync();
            ResetModalFlag();
        }

        private async Task ShowDialogueMessageAsync(string title, string message, Sprite sprite, string buttonLabel)
        {
            var tcs = new TaskCompletionSource<bool>();

            ShowCustomWindow(new ModalWindowData
            {
                Content = new ModalWindowContent
                {
                    Title = title,
                    Message = message,
                    Sprite = sprite
                },
                Layout = ModalWindowContentLayout.Vertical,
                ConfirmBtnData = new ModalButtonData(buttonLabel, () => tcs.TrySetResult(true))
            });

            await ShowActiveWindowAsync();
            await tcs.Task;
        }

        public void ShowCustomWindow(ModalWindowData data)
        {
            ModalWindow window = GetOrCreateModalWindow();
            window.Setup(data);
        }

        private async Task ShowActiveWindowAsync()
        {
            if (activeWindow == null || activeWindow.gameObject.transform.localScale == Vector3.one)
                return;

            await AnimateScaleAsync(activeWindow.gameObject, _from, _to);
        }

        private async Task HideActiveWindowAsync()
        {
            if (activeWindow == null)
                return;

            await AnimateScaleAsync(activeWindow.gameObject, _to, _from);
            activeWindow.gameObject.SetActive(false);
        }

        private  Task AnimateScaleAsync(GameObject target, Vector3 from, Vector3 to)
            => LeanTweenWrapper.AnimateAsync(target, _animationSettings, from, to, applyStartValue: _startPositionOffset);

        private ModalWindow GetOrCreateModalWindow()
        {
            if (activeWindow == null)
            {
                activeWindow = Instantiate(_modalWindowPrefab, _modalWindowParent);
                activeWindow.gameObject.transform.localScale = Vector3.zero;
            }

            return activeWindow;
        }

        private async Task AwaitModalReadyAsync()
        {
            while (isShowingModal) 
                await Task.Yield();

            isShowingModal = true;
        }

        private void ResetModalFlag() => isShowingModal = false;
    }
}
