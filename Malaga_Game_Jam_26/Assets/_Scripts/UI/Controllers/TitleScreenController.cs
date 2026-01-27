using UnityEngine;

namespace Bas.Pennings.UnityTools
{
    public class TitleScreenController : MonoBehaviour
    {

        private async void Update()
        {
            await ModalWindowService.Instance.ShowDialogueAsync(
                "Temporary Dialogue",
                new string[]
                {
                    "Bla bla 1",
                    "Bla bla 2",
                    "Bla bla 3",
                    "Bla bla 4",
                    "Bla bla 5",
                },
                new Sprite[]
                {
                    null,
                    null,
                    null,
                    null,
                    null
                });
        }

//        private async void HandleExit()
//            => await ModalWindowService.Instance.ShowConfirmationAsync(
//                "Exit Game",
//                "Do you really want to exit Unity Tools Sample?",
//                ExitGame, null);

//        public void ExitGame() =>
//#if UNITY_EDITOR
//            EditorApplication.isPlaying = false;
//#else
//            Application.Quit();
//#endif
    }
}
