using UnityEngine;

public class DoorMirror : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

    public CameraControl cameraControl;
    public Camera cam;

    private bool interactable = true;
    private PlayerMovement playerMovement;
    [SerializeField] private Collider doorCol;

    public Transform GetTransform()
    {
        return transform;
    }
    public void OnInteract(in PlayerMovement playerMovement)
    {   
        if (!interactable) 
            return;
        interactable = false;
        InInteract = true;
        Debug.Log("Interacted with " + name);
        cameraControl.SwitchToFixedCamera(cam);        
        doorCol.enabled = false; //disables door collider
    }

    private void Update()
    {
        if (InInteract)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                InInteract = false;
                interactable = true;
                doorCol.enabled = true; //enables door collider again
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                interactable = true;
                playerMovement.CanMove = true;
                cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
            }
        }
    }
}
