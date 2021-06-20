using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageController : MonoBehaviour, IPointerClickHandler
{
    private const float MOVEMENT_TIME = 0.5f;
    private float distance;
    private Transform slot;
    private Vector3 lastPosition;
    private bool moveImage;
    private AudioManager audioManager;
    private Animator animator;
    public Vector3 targetPosition;
    [SerializeField] private EventTrigger imageChangedPositionEvent;
    public Vector3 CorrectPosition { get; private set; }

    private void Awake()
    {
        slot = GameObject.FindWithTag("Slot").transform;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateCorrectPosition();
    }

    private void UpdateCorrectPosition()
    {
        switch (gameObject.name)
        {
            case "Image 1":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 1").transform.position;
                    break;
                }
            case "Image 2":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 2").transform.position;
                    break;
                }
            case "Image 3":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 3").transform.position;
                    break;
                }
            case "Image 4":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 4").transform.position;
                    break;
                }
            case "Image 5":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 5").transform.position;
                    break;
                }
            case "Image 6":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 6").transform.position;
                    break;
                }
            case "Image 7":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 7").transform.position;
                    break;
                }
            case "Image 8":
                {
                    CorrectPosition = transform.parent.Find("Reference Point 8").transform.position;
                    break;
                }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateCorrectPosition();
        CheckDistance();
    }

    private void UpdateDistanceReference()
    {
        Vector2 referencePoint1 = transform.parent.Find("Reference Point 1").transform.position;
        Vector2 referencePoint2 = transform.parent.Find("Reference Point 2").transform.position;
        distance = Mathf.CeilToInt(Vector2.Distance(referencePoint1, referencePoint2));
    }

    private void CheckDistance()
    {
        if (IsCloseToSlot())
        {
            lastPosition = transform.position;
            targetPosition = slot.position;
            slot.position = lastPosition;
            StartCoroutine(TriggerEvent());
        }
    }

    private bool IsCloseToSlot()
    {
        UpdateDistanceReference();
        return Mathf.CeilToInt(Vector2.Distance(transform.position, slot.position)) == distance;
    }

    private IEnumerator TriggerEvent()
    {
        moveImage = true;
        PlaySwitchSound();
        yield return new WaitForSeconds(MOVEMENT_TIME);
        imageChangedPositionEvent.Trigger();
    }

    private void PlaySwitchSound()
    {
        if (audioManager == null)
            audioManager = AudioManager.Instance;

        audioManager.PlayRandomSwitchSound();
    }

    private void Update()
    {
        if (moveImage)
        {
            transform.position = Vector3.Slerp(transform.position, targetPosition, MOVEMENT_TIME);
            if (transform.position == targetPosition)
            {
                moveImage = false;
            }
        }
    }

    public void Solve()
    {
        UpdateCorrectPosition();
        transform.position = CorrectPosition;
        targetPosition = CorrectPosition;
        UpdateAnimation();
        StartCoroutine(TriggerEventWithDelay());
    }

    private IEnumerator TriggerEventWithDelay()
    {
        yield return new WaitForSeconds(MOVEMENT_TIME);
        imageChangedPositionEvent.Trigger();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        targetPosition = position;
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        animator.SetBool("Can Move", IsCloseToSlot());
    }
}
