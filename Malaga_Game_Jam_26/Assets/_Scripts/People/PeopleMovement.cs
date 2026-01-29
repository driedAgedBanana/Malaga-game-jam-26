using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public enum NPCState
{
    IdleAtStart,      // Waiting to be called
    MovingToMiddle,   // Being called
    WaitingDecision,  // Player can accept/reject
    LeavingAccepted,  // Going to end point (right side)
    LeavingRejected   // Going back to start
}

public class PeopleMovement : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    private Animator animator;
    private bool isChosen = false;

    [Header("Movement")]
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;

    public float speed = 5f;
    private bool _isMoving = false;
    private Coroutine _moveCoroutine;
    private bool _npcWaitingForDecision = false;
    private NPCState _state = NPCState.IdleAtStart;


    [Header("Visuals")]
    public SpriteRenderer baseNPC;
    public SpriteRenderer leftEye;
    public SpriteRenderer rightEye;
    [Space]
    public SpriteRenderer baseNPCVisual;
    public SpriteRenderer leftEyeVisual;
    public SpriteRenderer rightEyeVisual;

    public UnityEvent OnOffScreen;

    private void Start()
    {
        animator = GetComponent<Animator>();
        npc.transform.position = startPoint.position;
    }

    private void Update()
    {

    }

    public void CallingNPCOnStart()
    {
        if (_state != NPCState.IdleAtStart) return;

        _state = NPCState.MovingToMiddle;

        _moveCoroutine = StartCoroutine(MovingNPC(middlePoint, () =>
        {
            _state = NPCState.WaitingDecision;
        }));

        EnableVisual();
    }

    public void AcceptNPC()
    {
        if (_state != NPCState.WaitingDecision) return;

        _state = NPCState.LeavingAccepted;

        _moveCoroutine = StartCoroutine(MovingNPC(endPoint, () =>
        {
            // Instantly reset NPC back to start after exiting right
            npc.transform.position = startPoint.position;
            DisableVisual();
            _state = NPCState.IdleAtStart;
        }));
    }


    public void RejectNPC()
    {
        if (_state != NPCState.WaitingDecision) return;

        _state = NPCState.LeavingRejected;

        _moveCoroutine = StartCoroutine(MovingNPC(startPoint, () =>
        {
            DisableVisual();
            _state = NPCState.IdleAtStart;
        }));
    }


    public void MoveNPC(Transform targetPosition)
    {
        if (!_isMoving) return;
        npc.transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            _isMoving = false;
        }
    }

    private IEnumerator MovingNPC(Transform targetPosition, System.Action onArrived = null)
    {
        while (Vector3.Distance(npc.transform.position, targetPosition.position) > 0.01f)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position,
                targetPosition.position,
                speed * Time.deltaTime);

            yield return null;
        }

        npc.transform.position = targetPosition.position;
        _moveCoroutine = null;
        onArrived?.Invoke();
        OnOffScreen?.Invoke();
    }


    private void EnableVisual()
    {
        if (baseNPCVisual != null && rightEyeVisual != null && leftEyeVisual != null)
        {
            baseNPC.sprite = baseNPCVisual.sprite;
            rightEye.sprite = rightEyeVisual.sprite;
            leftEye.sprite = leftEyeVisual.sprite;
        }
    }

    private void DisableVisual()
    {
        if (baseNPC != null && rightEye != null && leftEye != null)
        {
            baseNPC.sprite = null;
            rightEye.sprite = null;
            leftEye.sprite = null;
        }
    }

}
