using UnityEngine;

public class SideToSideMovement : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float speed = 2f;

    private Vector3 target;

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Assign pointA and pointB.");
            enabled = false;
            return;
        }

        transform.position = pointA.position;
        target = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
}
