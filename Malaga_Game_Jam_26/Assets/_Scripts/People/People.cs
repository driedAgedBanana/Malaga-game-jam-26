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
        public Texture2D Image;
        public Texture2D Eyes;
        public Texture2D Jacket;

        public Imigrant(string firstName, string lastName, string personality, bool isZombie,
                        Texture2D image, Texture2D eyes, Texture2D jacket)
        {
            FirstName = firstName;
            LastName = lastName;
            Personality = personality;
            IsZombie = isZombie;
            Image = image;
            Eyes = eyes;
            Jacket = jacket;
        }
    }

    private void Start()
    {
        // Create a test immigrant (replace textures in Inspector later)
        Imigrant test = new Imigrant("John", "Doe", "Calm", false, null, null, null);

        _visualizer.Visualize(test);
    }
}
