using UnityEngine;

public class LockPuzzle : MonoBehaviour
{
    [Header("Lock ID (optional, just for debug)")]
    public string lockName = "Lock";

    [Header("State")]
    public bool isSolved = false;

    [Header("Reference to Manager")]
    public DoorLockManager manager;

    // Call this when the player successfully solves this lock
    public void MarkAsSolved()
    {
        if (isSolved) return;

        isSolved = true;
        Debug.Log($"[LockPuzzle] {lockName} solved!");

        if (manager != null)
        {
            manager.OnLockSolved(this);
        }
        else
        {
            Debug.LogWarning($"[LockPuzzle] {lockName} has no DoorLockManager assigned!");
        }
    }
}
