using UnityEngine;

public class BlackDebug : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

    private void Start()
    {
        blackScreen.SetActive(true);
    }
}
