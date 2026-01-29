using System.Collections.Generic;
using UnityEngine;

public class RandomizePeople : MonoBehaviour
{
    [SerializeField] private List<string> names = new List<string>();
    [SerializeField] private List<string> lastNames = new List<string>();
    [SerializeField] private List<string> personalities = new List<string>();
    [SerializeField] private List<TransformedImage> bases = new List<TransformedImage>();

    [SerializeField] private List<TransformedImage> eyes = new List<TransformedImage>();
    [SerializeField] private List<TransformedImage> masks = new List<TransformedImage>();
    [SerializeField] private List<TransformedImage> jackets = new List<TransformedImage>();
    private List<People.Imigrant> imigrants = new List<People.Imigrant>();

    [SerializeField] private float immigrantAmount;

    private void Start()
    {
        addImmigrants();
    }
    private void addImmigrants()
    {
        for (int i = 0; i < immigrantAmount; i++)
        {
            RandomizeImmigrant();
        }
    }
    private void RandomizeImmigrant()
    {
        string name = names[Random.Range(0, names.Count)];
        string lastName = lastNames[Random.Range(0, lastNames.Count)];
        string personality = personalities[Random.Range(0, personalities.Count)];
        bool isZombie = Random.Range(0, 2) == 1;
        Texture2D image = bases[Random.Range(0, bases.Count)].Image;
        Texture2D eye = eyes[Random.Range(0, eyes.Count)].Image;
        Texture2D jacket = jackets[Random.Range(0, jackets.Count)].Image;
        Texture2D mask = masks[Random.Range(0, jackets.Count)].Image;

        People.Imigrant person = new People.Imigrant(name, lastName, personality, isZombie, image, eye, jacket, mask);
        imigrants.Add(person);
    }
}

[System.Serializable]
public record TransformedImage
{
    public Texture2D Image;
    public Vector3 Scale;
    public Vector3 Position;

    public TransformedImage(Texture2D image, Vector3 scale, Vector3 position)
        => (Image, Scale, Position) = (image, scale, position);
}
