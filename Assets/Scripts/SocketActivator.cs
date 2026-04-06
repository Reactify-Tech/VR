using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketActivator : MonoBehaviour
{
    public PlatformMover platform;
    public string requiredTag = "KeyItem";

    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();

    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnItemPlaced);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnItemPlaced);
        socket.selectExited.RemoveListener(OnItemRemoved);
    }
    
    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        Debug.Log("ITEM PLACED IN SOCKET");

        if (args.interactableObject.transform.CompareTag(requiredTag))
        {
            Debug.Log("CORRECT ITEM PLACED, ACTIVATING PLATFORM");
            platform.ActivatePlatform();

            LockOnPlace lockScript = args.interactableObject.transform.GetComponent<LockOnPlace>();
            if (lockScript != null)
            {
                lockScript.Lock();
            }
        }
    }

    private void OnItemRemoved(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag(requiredTag))
        {
            platform.DeactivatePlatform();
        }
    }
}
