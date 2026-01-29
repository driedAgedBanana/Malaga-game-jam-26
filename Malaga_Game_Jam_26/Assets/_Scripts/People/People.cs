using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private ImmigrantVisualizer _visualizer;

    public class Imigrant
    {
        public string FirstName;
        public string LastName;
        public string Personality;
        public bool IsZombie;
        public Sprite BaseImg;
        public Sprite Eyes;
        public Sprite Jacket;

        // Constructor
        public Imigrant(string firstName, string lastName, string personality, bool isZombie, Sprite image, Sprite eyes, Sprite jacket, Sprite baseImg)
        {
            FirstName = firstName;
            LastName = lastName;
            Personality = personality;
            IsZombie = isZombie;
            BaseImg = image;
            Eyes = eyes;
            Jacket = jacket;
            BaseImg = baseImg;
        }
    }

    private void Start()
    {
        // Create a test immigrant (replace textures in Inspector later)
        Imigrant test = new Imigrant("John", "Doe", "Calm", false, null, null, null, null);

        _visualizer.Visualize();
    }
}
