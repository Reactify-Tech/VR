using UnityEngine;

public class ShowTextOnApproach : MonoBehaviour
{
    [SerializeField] private GameObject textBox;

    private void Start()
    {
        textBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false);
        }
    }

}
