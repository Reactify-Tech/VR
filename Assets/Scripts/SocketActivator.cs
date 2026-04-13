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
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnItemPlaced);
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        Transform item = args.interactableObject.transform;

        if (!item.CompareTag(requiredTag))
            return;

        Transform target = socket.attachTransform != null ? socket.attachTransform : transform;

        item.SetPositionAndRotation(target.position, target.rotation);

        if (platform != null)
            platform.ActivatePlatform();
    }
}