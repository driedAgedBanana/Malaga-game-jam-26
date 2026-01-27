using UnityEngine;
using UnityEngine.UIElements;
using static People;

public class ImmigrantVisualizer : MonoBehaviour
{
    [Header("References to object images")]
    [SerializeField] private Image _eyesImg;
    [SerializeField] private Image _jacketImg;

    public void Visualize(Imigrant imigrant)
    {
        _eyesImg.image = imigrant.Eyes;
        _jacketImg.image = imigrant.Jacket;
    }
}
