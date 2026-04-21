using UnityEngine;
using UnityEngine.Events;

public class VRPressButton : MonoBehaviour
{
    [Header("Movement")]
    public Transform buttoncap;
    public Vector3 localPressAxis = new Vector3(0f, -1f, 0f);
    public float pressDepth = 0.02f;
    public float pressSpeed = 8f;
    public float releaseSpeed = 10f;

    [Header("Press Logic")]
    public float pressedThreshold = 0.015f;
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private Vector3 startLocalPos;
    private bool isTouching = false;
    private bool isPressed = false;
    private float currentDepth = 0f;

    private void Start()
    {
        if (buttoncap == null)
            buttoncap = transform;

        startLocalPos = buttoncap.localPosition;
        localPressAxis = localPressAxis.normalized;
    }

    private void Update()
    {
        float  targetDepth = isTouching ? pressDepth : 0f;
        float speed = isTouching ? pressSpeed : releaseSpeed;

        currentDepth = Mathf.MoveTowards(currentDepth, targetDepth, speed * Time.deltaTime);
        buttoncap.localPosition = startLocalPos + localPressAxis * currentDepth;

        bool nowPressed = currentDepth >= pressedThreshold;

        if (nowPressed && !isPressed)
        {
            isPressed = true;
            onPressed.Invoke();
        }
        else if (!nowPressed && isPressed)
        {
            isPressed = false;
            onReleased.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
            isTouching = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
            isTouching = false;
    }                                                                                                                                                                                                        
}
