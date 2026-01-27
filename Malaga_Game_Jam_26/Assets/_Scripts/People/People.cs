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
        public Texture2D LeftEye;
        public Texture2D RightEye;

        // Constructor
        public Imigrant(string firstName, string lastName, string personality, bool isZombie, Texture2D image, Texture2D leftEye, Texture2D rightEye)
        {
            FirstName = firstName;
            LastName = lastName;
            Personality = personality;
            IsZombie = isZombie;
            Image = image;
            LeftEye = leftEye;
            RightEye = rightEye;
        }
    }

}
