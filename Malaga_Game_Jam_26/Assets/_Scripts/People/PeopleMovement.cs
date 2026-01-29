using System.Collections;
using UnityEngine;

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

    [Header("Visuals")]
    public SpriteRenderer baseNPC;
    public SpriteRenderer leftEye;
    public SpriteRenderer rightEye;
    [Space]
    public SpriteRenderer baseNPCVisual;
    public SpriteRenderer leftEyeVisual;
    public SpriteRenderer rightEyeVisual;

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
        if (_moveCoroutine != null) return;
        _moveCoroutine = StartCoroutine(MovingNPC(middlePoint));

        EnableVisual();
    }

    public void AcceptNPC()
    {
        if (_moveCoroutine != null) return;
        _moveCoroutine = StartCoroutine(MovingNPC(endPoint));

        Invoke(nameof(DisableVisual), 3f);
    }

    public void RejectNPC()
    {
        if (_moveCoroutine != null) return;
        _moveCoroutine = StartCoroutine(MovingNPC(startPoint));

        Invoke(nameof(DisableVisual), 1.5f);
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

    private IEnumerator MovingNPC(Transform targetPosition)
    {
        while (Vector3.Distance(npc.transform.position, targetPosition.position) > 0.01f)
        {
            npc.transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition.position;
        _moveCoroutine = null;
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
