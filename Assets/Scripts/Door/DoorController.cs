//using System.Diagnostics;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door State")]
    public bool isLocked = true;
    public bool isOpen = false;

    [Header("Optional: Visuals / Animations")]
    public Animator animator;   // if you have an Animator
    public string openTriggerName = "Open";

    public void UnlockDoor()
    {
        if (!isLocked) return; // already unlocked

        isLocked = false;
        Debug.Log("[DoorController] Door unlocked!");

        // If you want it to open immediately when unlocked:
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        if (isLocked)
        {
            Debug.Log("[DoorController] Door is locked, cannot open yet.");
            return;
        }

        isOpen = true;
        Debug.Log("[DoorController] Door opened!");

        // Play animation if assigned
        if (animator != null && !string.IsNullOrEmpty(openTriggerName))
        {
            animator.SetTrigger(openTriggerName);
        }

        // OR: enable collider, move door, disable collider, etc.
        // e.g. Destroy(GetComponent<Collider2D>());
        // e.g. transform.position += new Vector3(0, 3f, 0);
    }
}
