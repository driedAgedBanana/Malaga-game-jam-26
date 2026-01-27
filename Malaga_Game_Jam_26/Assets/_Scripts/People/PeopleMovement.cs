using UnityEngine;

public class PeopleMovement : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform middle;
    [SerializeField] private Transform end;
    [SerializeField] private GameObject npc;

    private void Start()
    {
        SpawnNPC();
    }

    public void SpawnNPC()
    {
        npc = Instantiate(npc, start.position, Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            MoveNPC();
        }
    }

    public void MoveNPC()
    {
        npc.transform.position = Vector3.Lerp(start.position, middle.position, 100f);
    }
}
