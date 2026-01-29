using UnityEngine;
using UnityEngine.UI;

public class ImmigrantVisualizer : MonoBehaviour
{
    [Header("References to object images")]
    [SerializeField] private GameObject _immigrantParent;
    [SerializeField] private GameObject[] _immigrants;

    [SerializeField] private RandomizePeople _randomizePeople;
    private GameObject displayedImmigrant = null;

    bool usingDesignOne = true;

    private void Start()
    {
        Visualize();
    }

    public void Visualize()
    {
        if (displayedImmigrant != null) 
        {
            Destroy(displayedImmigrant);
        }

        GameObject objectToSpawn = usingDesignOne ? _immigrants[1] : _immigrants[0];
        usingDesignOne = !usingDesignOne;

        displayedImmigrant = Instantiate(objectToSpawn);

        _immigrantParent.transform.parent = displayedImmigrant.transform;
        displayedImmigrant.transform.localPosition = Vector3.zero;
    }
}
