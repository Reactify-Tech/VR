using UnityEngine;

public class MultiButtonDoorController : MonoBehaviour
{
    [Header("References")]
    public Door targetDoor;

    [Header("Settings")]
    public int numberOfButtons = 1;

    [Header("Debug")]
    [SerializeField] private bool[] buttonStates;

    public void InitializeButtons(int count)
    {
        buttonStates = new bool[count];
    }

    public void SetButtonState(int buttonIndex, bool isPressed)
    {
        if (buttonStates == null || buttonStates.Length == 0)
        {
            Debug.LogWarning("Button states not initialized.");
            return;
        }

        if (buttonIndex < 0 || buttonIndex >= buttonStates.Length)
        {
            Debug.LogWarning("Button index out of range.");
            return;
        }

        buttonStates[buttonIndex] = isPressed;
        EvaluateDoorState();
    }

    private void EvaluateDoorState()
    {
        for (int i = 0; i < buttonStates.Length; i++)
        {
            if (!buttonStates[i])
            {
                targetDoor.CloseDoor();
                return;
            }
        }

        targetDoor.OpenDoor();
    }
}
