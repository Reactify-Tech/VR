using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 3, 0);
    public float speed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool opening;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
    }

    void Update()
    {
        if (opening)
            transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * speed);
    }

    public void Open()
    {
        opening = true;
    }
}
