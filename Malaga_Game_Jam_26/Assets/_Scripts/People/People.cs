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
        public Texture2D BaseImg;
        public Texture2D Eyes;
        public Texture2D Jacket;

        // Constructor
        public Imigrant(string firstName, string lastName, string personality, bool isZombie, Texture2D image, Texture2D eyes, Texture2D jacket, Texture2D baseImg)
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

        _visualizer.Visualize(test);
    }
}
