using UnityEngine;
using System.Collections.Generic;

public class RecyclingMachine : MonoBehaviour
{
    [Header("Recycling Goal")]
    [SerializeField] private int targetCount = 3;

    [Header("Platform")]
    [SerializeField] private Transform platform;
    [SerializeField] private Transform platformTarget;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Optional")]
    [SerializeField] private bool destroyRecycledObjects = true;

    private int currentCount = 0;
    private bool unlocked = false;
   
    private void OnTriggerEnter(Collider other)
    {
        // only count objects labelled as recyclable
        if (!other.CompareTag("Recyclable"))
            return;

        RecyclableItem item = other.GetComponent<RecyclableItem>();
        if (item == null || item.hasBeenRecycled)
            return;

        item.hasBeenRecycled = true;
        currentCount++;

        Debug.Log($"Recycled: {currentCount}/{targetCount}");
        
        if (destroyRecycledObjects)
            Destroy(other.gameObject);

        if (currentCount >= targetCount)
            UnlockPath();
    }

    private void UnlockPath()
    {
        if (unlocked)
            return;
        unlocked = true;
        Debug.Log("Path unlocked");
    }

    private void Update()
    {
        if (!unlocked || platform == null || platformTarget == null)
            return;

        platform.position = Vector3.MoveTowards(platform.position, platformTarget.position, moveSpeed * Time.deltaTime);
    }

}
