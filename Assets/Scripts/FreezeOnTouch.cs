using UnityEngine;

public class FreezeOnTouch : MonoBehaviour
{
    private bool frozen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (frozen) return;

        frozen = true;
        Time.timeScale = 0f; // Freeze the game
    }
}
