using UnityEngine;

public class Bed : MonoBehaviour,IInteractable

{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public CameraControl cameraControl;
    public Camera cam;

    private bool interactable = true;
    private PlayerMovement playerMovement;

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        interactable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.playerMovement = playerMovement;
        this.playerMovement.CanMove = false;
        cameraControl.SwitchToFixedCamera(cam);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            interactable = true;
            playerMovement.CanMove = true;
            cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);

        }
    }

}
