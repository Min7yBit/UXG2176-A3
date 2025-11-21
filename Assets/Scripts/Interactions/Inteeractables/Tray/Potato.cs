using UnityEngine;
using System.Collections;

public class Potato : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public float zoomDuration = 1.0f;
    public Vector3 zoomedPosition;
    public Quaternion rotatePosition;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Renderer Rrenderer;
    [SerializeField]private bool mouseOver = false;
    private bool interactable = true;
    [SerializeField]private bool zoomedIn = false;
    private bool allowRotation = false;

    [Header("Rotation Settings")]
    public int intervals = 4;
    public float rotationDuration = 0.25f;
    [SerializeField] private int currentStepIndex = 0; // Tracks which step we are currently at
    private float stepAngle;         // The angle of a single step (360 / intervals)
    private Coroutine rotateCoroutine;

    private void Awake()
    {
        Rrenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Safety check to prevent division by zero and nonsensical rotations
        if (intervals <= 0)
        {
            Debug.LogError("Intervals must be a positive integer!");
            intervals = 1;
        }
        // Calculate the fixed angle for each interval
        stepAngle = 360f / intervals;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Potato");
        Rrenderer.material.color = Color.yellow;
        mouseOver = true;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && mouseOver && zoomedIn)
        {
            RotateToNextInterval(); //when zoomed in, left click to rotate
        }
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Potato");
        Rrenderer.material.color = Color.white;
        mouseOver = false;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        //must be in contact with it to work!!!

        if (zoomedIn && !mouseOver) //click anywhere but the potato to zoom out
        {
            interactable = false; 
            ZoomOut();
        }
        else if (!zoomedIn && mouseOver && interactable) //click on the potato to zoom in
        {
            interactable = false;
            ZoomIn();
        }
    }

    private void ZoomIn()
    {
        StartCoroutine(ZoomCoroutine(transform.position, zoomedPosition, zoomDuration));
        StartCoroutine(RotateCoroutine(transform.rotation, rotatePosition, zoomDuration));
    }
    private void ZoomOut()
    {
        StartCoroutine(ZoomCoroutine(transform.position, initialPosition, zoomDuration));
        StartCoroutine(RotateCoroutine(transform.rotation, initialRotation, zoomDuration));
    }
    private IEnumerator ZoomCoroutine(Vector3 startPos, Vector3 endPos, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPos;

        //toggles
        zoomedIn = !zoomedIn; 
        allowRotation = !allowRotation;
        interactable = true;
    }
    private IEnumerator RotateCoroutine(Quaternion startPos, Quaternion endPos, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.rotation = Quaternion.Lerp(startPos, endPos, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = endPos;
    }

    public void ResetPotato()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        zoomedIn = false;
        allowRotation = false;
        interactable = false;
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
            float t = timeElapsed / rotationDuration;

            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = endRot;
        rotateCoroutine = null; 
    }
}
