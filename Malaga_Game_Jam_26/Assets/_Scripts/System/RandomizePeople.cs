using System.Collections.Generic;
using UnityEngine;

public class RandomizePeople : MonoBehaviour
{
    [SerializeField] private List<string> names = new List<string>();
    [SerializeField] private List<string> lastNames = new List<string>();
    [SerializeField] private List<string> personalities = new List<string>();


    public void RandomizeImmigrant()
    {
        string name = names[Random.Range(0, names.Count)];
        string lastName = lastNames[Random.Range(0, lastNames.Count)];
        string personality = personalities[Random.Range(0, personalities.Count)];
        bool isZombie = Random.Range(0, 2) == 1;
    }
}
