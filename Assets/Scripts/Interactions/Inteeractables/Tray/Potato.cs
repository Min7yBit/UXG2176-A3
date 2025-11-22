using UnityEngine;
using System.Collections;

public class Potato : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

    public float zoomDuration = 1.0f;
    public Vector3 zoomedPosition;
    public Vector3 rotatePosition;

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
    private bool rotating = false;

    private void Awake()
    {
        Rrenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;

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
        StartCoroutine(ZoomCoroutine(transform.localPosition, zoomedPosition, zoomDuration));
        StartCoroutine(RotateCoroutine(transform.localRotation, Quaternion.Euler(rotatePosition), zoomDuration));
    }
    private void ZoomOut()
    {
        StartCoroutine(ZoomCoroutine(transform.localPosition, initialPosition, zoomDuration));
        StartCoroutine(RotateCoroutine(transform.localRotation, initialRotation, zoomDuration));
    }
    private IEnumerator ZoomCoroutine(Vector3 startPos, Vector3 endPos, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPos;

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
            transform.localRotation = Quaternion.Lerp(startPos, endPos, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localRotation = endPos;
    }

    public void ResetPotato()
    {
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
        zoomedIn = false;
        allowRotation = false;
        interactable = false;
    }

    private void RotateToNextInterval()
    {
        if (rotating)
            return;

        Debug.Log("X axis (Right) = " + transform.right);
        Debug.Log("Y axis (Up) = " + transform.up);
        Debug.Log("Z axis (Forward) = " + transform.forward);

        Quaternion startRotation = transform.localRotation;

        // Rotate around local X axis
        Quaternion stepRotation = Quaternion.AngleAxis(stepAngle, Vector3.up);

        Quaternion targetRotation = startRotation * stepRotation;

        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(RotateSmoothly(startRotation, targetRotation));

        currentStepIndex = (currentStepIndex + 1) % intervals;
    }

    IEnumerator RotateSmoothly(Quaternion startRot, Quaternion endRot)
    {
        rotating = true;
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRot;
        rotateCoroutine = null;
        rotating = false;
    }

}
