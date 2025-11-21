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
    private bool mouseOver = false;
    private bool interactable = true;
    private bool zoomedIn = false;
    private bool allowRotation = false;
    private void Awake()
    {
        Rrenderer = GetComponent<Renderer>();
    }
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
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
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Potato");
        Rrenderer.material.color = Color.white;
        mouseOver = false;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        Debug.Log("Interactable Bool: " + interactable);
        Debug.Log("MouseOver Bool: " + mouseOver);
        if (!interactable || !mouseOver)
            return;

        if (zoomedIn)
        {
            interactable = false; 
            ZoomOut();
        }
        else
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
}
