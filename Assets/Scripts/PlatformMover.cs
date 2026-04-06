using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector3 raisedPosition;
    public float speed = 2f;

    private Vector3 startPosition;
    private bool isMovingUp = false;

    void Start()
    {
        startPosition = transform.position;
    }

    public void ActivatePlatform()
    {
        isMovingUp = true;
    }

    public void DeactivatePlatform()
    {
        isMovingUp = false;
    }

    private void Update()
    {
        if (isMovingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, raisedPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }
    }
}

