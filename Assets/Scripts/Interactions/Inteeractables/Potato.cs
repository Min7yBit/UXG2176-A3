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

    [SerializeField] private bool interactable = true;
    private bool zoomedIn = false;
    private bool allowRotation = false;
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        Debug.Log("CLicked potato");
        if (!interactable)
            return;
        Debug.Log("Interacted with Potato");

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
