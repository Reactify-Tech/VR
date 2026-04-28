using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class VRCrankWheel : MonoBehaviour
{
    [Header("Refs")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable handle;
    public Transform wheelPivot;
    public Transform handAttachPoint;

    [Header("Crank Settings")]
    public float requiredTurns = 3f;
    public float wheelRadius = 0.35f;
    public bool clockwise = true;

    [Header("Output")]
    public UnityEvent onComplete;

    private Transform grabbingHand;
    private float totalDegrees;
    private float previousAngle;
    private bool isGrabbed;
    private bool completed;

    private void Awake()
    {
        handle.selectEntered.AddListener(OnGrab);
        handle.selectExited.AddListener(OnRelease);
    }

    private void Update()
    {
        if (!isGrabbed || completed || grabbingHand == null)
            return;

        Vector3 localHand = wheelPivot.InverseTransformPoint(grabbingHand.position);

        float currentAngle = Mathf.Atan2(localHand.y, localHand.x) * Mathf.Rad2Deg;
        float delta = Mathf.DeltaAngle(previousAngle, currentAngle);

        if (!clockwise)
            delta *= -1f;

        if (delta > 0)
        {
            totalDegrees += delta;
            wheelPivot.Rotate(Vector3.forward, clockwise ? delta : -delta);
        }

        previousAngle = currentAngle;

        if (totalDegrees >= requiredTurns * 360f)
        {
            completed = true;
            onComplete.Invoke();
        }
    }

    void OnGrab (SelectEnterEventArgs args)
    {
        grabbingHand = args.interactorObject.transform;
        Vector3 localHand = wheelPivot.InverseTransformPoint(grabbingHand.position);
        previousAngle = Mathf.Atan2(localHand.y, localHand.x) * Mathf.Rad2Deg;
        isGrabbed = true;
    }

    void OnRelease (SelectExitEventArgs args)
    {
        isGrabbed = false;
        grabbingHand = null;
    }

    public float GetProgress01()
    {
        return Mathf.Clamp01(totalDegrees / (requiredTurns * 360f));
    }
}
