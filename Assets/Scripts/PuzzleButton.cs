using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public MultiButtonDoorController controller;
    public int buttonIndex;

    private bool isPressed = false;

    public void Press()
    {
        if (isPressed) return;

        isPressed = true;
        controller.SetButtonState(buttonIndex, true);
    }

    public void Release()
    {
        if (!isPressed) return;

        isPressed = false;
        controller.SetButtonState(buttonIndex, false);
    }
}
