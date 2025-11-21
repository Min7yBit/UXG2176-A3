using UnityEngine;
using System.Collections; // Required for Coroutines

public class LockCombination : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Number of steps (intervals) in a full 360 rotation. (e.g., 4 means 90 degree snaps)")]
    public int intervals = 4;

    [Tooltip("Time in seconds for each rotation step to complete.")]
    public float rotationDuration = 0.25f;

    [Header("Debug/State")]
    [SerializeField]
    private int currentStepIndex = 0; // Tracks which step we are currently at

    private float stepAngle;         // The angle of a single step (360 / intervals)
    private Coroutine rotateCoroutine; // Reference to the running coroutine

    void Start()
    {
        // Safety check to prevent division by zero and nonsensical rotations
        if (intervals <= 0)
        {
            Debug.LogError("Intervals must be a positive integer!");
            intervals = 1;
        }

        // Calculate the fixed angle for each interval
        stepAngle = 360f / intervals;
    }

    void Update()
    {
        // Check for the Right Mouse Button press (Input.GetMouseButtonDown(1))
        if (Input.GetMouseButtonDown(1))
        {
            RotateToNextInterval();
        }
        // For testing: Reset combination when 'R' key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCombination();
        }
    }

    private void RotateToNextInterval()
    {
        float currentY = transform.localEulerAngles.y;
        float newTargetY = currentY + stepAngle;

        Quaternion startRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.Euler(
            transform.localEulerAngles.x,
            newTargetY,
            transform.localEulerAngles.z
        );

        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        rotateCoroutine = StartCoroutine(RotateSmoothly(startRotation, targetRotation));

        currentStepIndex = (currentStepIndex + 1) % intervals;
    }

    IEnumerator RotateSmoothly(Quaternion startRot, Quaternion endRot)
    {
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // Calculate the interpolation factor (t)
            float t = timeElapsed / rotationDuration;

            // Use Quaternion.Lerp (or Slerp for rotations) for the smooth movement
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            timeElapsed += Time.deltaTime;

            // Pause and wait for the next frame
            yield return null;
        }

        // Ensure the rotation lands exactly on the target angle
        transform.rotation = endRot;
        rotateCoroutine = null; // Mark the coroutine as finished
    }

    private void ResetCombination()
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
        Quaternion resetRotation = Quaternion.Euler(
            transform.localEulerAngles.x,
            0f,
            transform.localEulerAngles.z
        );
        rotateCoroutine = StartCoroutine(RotateSmoothly(transform.rotation, resetRotation));
        currentStepIndex = 0;
    }
}