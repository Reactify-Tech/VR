using UnityEngine;
using Unity.XR.CoreUtils;

public class MovingPlatformXRRide : MonoBehaviour
{
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private bool inheritRotation = false;

    private bool playerOnPlatform;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        if (xrOrigin == null)
        {
            var origin = FindFirstObjectByType<XROrigin>();
            if (origin != null)
                xrOrigin = origin.transform;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (xrOrigin == null)
            return;

        if (!playerOnPlatform)
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            return;
        }

        Vector3 deltaPos = transform.position - lastPosition;
        xrOrigin.position += deltaPos;

        if (inheritRotation)
        {
            Quaternion deltaRot = transform.rotation * Quaternion.Inverse(lastRotation);

            Vector3 pivot = transform.position;
            Vector3 offset = xrOrigin.position - pivot;
            offset = deltaRot * offset;
            xrOrigin.position = pivot + offset;
            xrOrigin.rotation = deltaRot * xrOrigin.rotation;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger: " + other.name);
        playerOnPlatform = true;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger: " + other.name);
        playerOnPlatform = false;
    }
}
