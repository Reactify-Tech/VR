using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorPivot;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    private void Start()
    {
        if (doorPivot == null)
            doorPivot = transform;
        
        closedRotation = doorPivot.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    private void Update()
    {
       Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
