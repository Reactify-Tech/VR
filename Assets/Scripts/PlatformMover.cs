using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Movement")]
    public Transform raisedTarget;
    public float speed = 2f;
    public bool returnWhenItemRemoved = false;

    private Vector3 _startPosition;
    private bool _moveUp = false;

    private void Start()
    {
        _startPosition = transform.position;
        if (raisedTarget == null)
        {
            Debug.LogError("PlatformMover: Raised Target is not assigned.", this);
        }
    }

    void Update()
    {
        if (raisedTarget == null) 
        return;

        Vector3 targetPosition = _moveUp ? raisedTarget.position : _startPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void ActivatePlatform()
    {
        Debug.Log("PlatformMover: ActivatePlatform called.", this);
        _moveUp = true;
    }

    public void DeactivatePlatform()
    {
        Debug.Log("PlatformMover: DeactivatePlatform called.", this);

        if (returnWhenItemRemoved)
        {
            _moveUp = false;
        }
    }
}