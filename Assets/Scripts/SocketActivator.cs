using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SocketActivator : MonoBehaviour
{
    [Header("Required Components")]
    public PlatformMover platform;
    public string requiredTag = "KeyItem";

    [Header("Optional")]
    public bool lockItemInPlace = true;

    private XRSocketInteractor _socket;

    private void Awake()
    {
        _socket = GetComponent<XRSocketInteractor>();
        if (_socket == null)
        {
            Debug.LogError("SocketActivator: No XRSocketInteractor found on this GameObject.", this);
        }

        if (platform == null)
        {
            Debug.LogError("SocketActivator: No PlatformMover assigned.", this);
        }
    }

    private void OnEnable()
    {
        if (_socket != null)
        {
            _socket.selectEntered.AddListener(OnItemPlaced);
            _socket.selectExited.AddListener(OnItemRemoved);
        }
    }

    private void OnDisable()
    {
        if (_socket != null)
        {
            _socket.selectEntered.RemoveListener(OnItemPlaced);
            _socket.selectExited.RemoveListener(OnItemRemoved);
        }
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        Transform itemTransform = args.interactableObject.transform;

        Debug.Log("SocketActivator: Item placed in socket: " + itemTransform.name, this);

        if (!itemTransform.CompareTag(requiredTag))
        {
            Debug.Log("SocketActivator: Placed item does not have the required tag: " + requiredTag, this);
            return;
        }

        Debug.Log("SocketActivator: Placed item has the required tag. Activating platform.", this);

        if (lockItemInPlace)
        {
            LockOnPlace lockScript = itemTransform.GetComponent<LockOnPlace>();
            if (lockScript != null)
            {
                lockScript.LockItem();
            }
        }

        if (platform != null)
        {
            platform.ActivatePlatform();
        }
    }

    private void OnItemRemoved(SelectExitEventArgs args)
    {
        Transform itemTransform = args.interactableObject.transform;

        Debug.Log("SocketActivator: Item removed from socket: " + itemTransform.name, this);

        if (!itemTransform.CompareTag(requiredTag))
            return;

        if (platform != null)
        {
            platform.DeactivatePlatform();
        }
    }
}