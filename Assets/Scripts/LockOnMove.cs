using UnityEngine;


public class LockOnPlace : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grabInteractable;
    private Rigidbody _rb;

    private void Awake()
    {
        _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        _rb = GetComponent<Rigidbody>();
    }

    public void LockItem()
    {
        Debug.Log("LockOnPlace: LockItem called on " + gameObject.name, this);

        if (_rb != null)
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        if (_grabInteractable != null)
        {
            _grabInteractable.enabled = false;
        }
    }
}