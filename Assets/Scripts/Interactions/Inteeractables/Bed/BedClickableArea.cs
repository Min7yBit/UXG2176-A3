using UnityEngine;

public class BedClickableArea : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public CameraControl cameraControl;
    public Camera cam;

    private bool interactable = true;
    private bool mouseOver = false;
    private PlayerMovement playerMovement;
    private Collider col;
    [SerializeField]private Collider bedCol;
    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public Transform GetTransform()
    {
        return transform;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Bed Clickable Area");
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Bed Clickable Area");
        mouseOver = false;
    }
    public void OnInteract(in PlayerMovement playerMovement)
    {
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Interacted with " + name);
            cameraControl.SwitchToFixedCamera(cam);
            col.enabled = false; //disables interaction so can interact with child components
            bedCol.enabled = false; //disables bed collider
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            col.enabled = true; //enables interaction again
            bedCol.enabled = true; //enables bed collider again
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            interactable = true;
            playerMovement.CanMove = true;
            cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
        }
    }
}
