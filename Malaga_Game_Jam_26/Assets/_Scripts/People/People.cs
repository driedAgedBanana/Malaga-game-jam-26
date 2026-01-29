using UnityEngine;

public class People : MonoBehaviour
{
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

}
