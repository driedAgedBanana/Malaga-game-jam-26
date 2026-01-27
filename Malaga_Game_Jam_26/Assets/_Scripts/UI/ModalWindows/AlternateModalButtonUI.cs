using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bas.Pennings.UnityTools
{
    public class AlternateModalButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonLabel;

        public Button Button => _button;
        public TextMeshProUGUI ButtonLabel => _buttonLabel;
    }
}
