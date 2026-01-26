using UnityEngine;

public class People : MonoBehaviour
{
    public class Imigrant
    {
        public string FirstName;
        public string LastName;
        public string Personality;
        public bool IsZombie;

        // Constructor
        public Imigrant(string firstName, string lastName, string personality, bool isZombie)
        {
            FirstName = firstName;
            LastName = lastName;
            Personality = personality;
            IsZombie = isZombie;
        }
    }

}
