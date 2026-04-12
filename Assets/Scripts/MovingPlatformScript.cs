using UnityEngine;
using Unity.XR.CoreUtils;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private Collider rideTrigger;
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
        Quaternion deltaRot = transform.rotation * Quaternion.Inverse(lastRotation);

        xrOrigin.position += deltaPos;

        if (inheritRotation)
        {
            Vector3 pivot = transform.position;
            Vector3 offset = xrOrigin.position - pivot;
            offset = deltaRot * offset;
            xrOrigin.position = pivot + offset;
            xrOrigin.rotation = deltaRot * xrOrigin.rotation;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private bool IsPlayer(Collider other)
    {
        return other.GetComponentInParent<XROrigin>() != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            playerOnPlatform = true;
            lastPosition = transform.position;
            lastRotation = transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            playerOnPlatform = false;
        }
    }
}
