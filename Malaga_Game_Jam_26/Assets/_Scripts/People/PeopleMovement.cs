using System.Runtime.CompilerServices;
using UnityEngine;

public class PeopleMovement : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    private Animator animator;
    private bool isChosen = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PassNPC();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FailNPC();
        }
    }
    public void FailNPC()
    {
        if (!isChosen)
        {
            isChosen = true;
            animator.SetTrigger("MoveFail");
        }
        
    }
    public void PassNPC()
    {
        if (!isChosen)
        {
            isChosen = true;
            animator.SetTrigger("MovePass");
        }
        
        
    }

    public void Restart()
    {
        //transform.position = new Vector2(-34f, transform.position.y);
        animator.Play("MoveStart");
        isChosen = false;
    }

}
