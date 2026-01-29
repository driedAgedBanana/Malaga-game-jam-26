using UnityEngine;
using UnityEngine.UIElements;

public class ImmigrantVisualizer : MonoBehaviour
{
    [SerializeField] private Image _eyesImg;
    [SerializeField] private Image _jacketImg;

    public void Visualize(People.Imigrant imigrant)
    {
        _eyesImg.image = imigrant.Eyes;
        _jacketImg.image = imigrant.Jacket;
    }
}
