using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Angle)")]
    public float swayAngle = 20f;
    public float swaySpeed = 1f;

    [Header("Floating")]
    public float bobHeight = 0.05f;
    public float bobSpeed = 1.5f;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // rotate side to side
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAngle;
        transform.rotation = startRotation * Quaternion.Euler(0, 0, sway);

        // bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);


    }
}
