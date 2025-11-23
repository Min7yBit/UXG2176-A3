using UnityEngine;

public class DoorLockManager : MonoBehaviour
{
    [Header("Door to unlock")]
    public DoorController door;

    [Header("Locks tied to the door")]
    public LockPuzzle[] locks; // size = 3 in Inspector

    private bool doorUnlocked = false;

    private void Awake()
    {
        // Optional: auto-wire manager into each LockPuzzle so you don't forget
        if (locks != null)
        {
            foreach (var lp in locks)
            {
                if (lp != null)
                {
                    lp.manager = this;
                }
            }
        }
    }

    public void OnLockSolved(LockPuzzle solvedLock)
    {
        Debug.Log($"[DoorLockManager] Received solved event from {solvedLock.lockName}");

        if (doorUnlocked) return; // already unlocked earlier

        if (AllLocksSolved())
        {
            doorUnlocked = true;
            Debug.Log("[DoorLockManager] All locks solved! Unlocking door...");
            if (door != null)
            {
                door.UnlockDoor();
            }
            else
            {
                Debug.LogWarning("[DoorLockManager] No DoorController assigned!");
            }
        }
        else
        {
            Debug.Log("[DoorLockManager] Not all locks solved yet.");
        }
    }

    private bool AllLocksSolved()
    {
        if (locks == null || locks.Length == 0)
        {
            Debug.LogWarning("[DoorLockManager] No locks assigned!");
            return false;
        }

        foreach (var lp in locks)
        {
            if (lp == null)
            {
                Debug.LogWarning("[DoorLockManager] A lock slot is empty!");
                return false;
            }

            if (!lp.isSolved)
                return false;
        }

        return true;
    }
}
