using UnityEngine;

public class People : MonoBehaviour
{
    public class Imigrant
    {
        public string FirstName;
        public string LastName;
        public string Personality;
        public bool IsZombie;
        public Texture2D Image;

        // Constructor
        public Imigrant(string firstName, string lastName, string personality, bool isZombie, Texture2D image)
        {
            FirstName = firstName;
            LastName = lastName;
            Personality = personality;
            IsZombie = isZombie;
            Image = image;
        }
    }

}
