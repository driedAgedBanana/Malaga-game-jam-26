using UnityEngine;
using UnityEngine.UIElements;
using static People;

public class ImmigrantVisualizer : MonoBehaviour
{
    [Header("References to object images")]
    [SerializeField] private Image _baseImg;
    [SerializeField] private Image _eyesImg;
    [SerializeField] private Image _maskImg;
    [SerializeField] private Image _clothingImg;

    public void Visualize(Imigrant imigrant)
    {
        _baseImg.image = imigrant.BaseImg;
        _eyesImg.image = imigrant.Eyes;
        _maskImg.image = imigrant.BaseImg;
        _clothingImg.image = imigrant.Jacket;
    }
}
