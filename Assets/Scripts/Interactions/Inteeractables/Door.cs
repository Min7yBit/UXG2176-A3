using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public CameraControl cameraControl;
    public Camera cam;
    public GameObject lockReflection;

    public string itemName;

    private PlayerMovement playerMovement;
    private bool interactable = true;
    [SerializeField]private Inventory inventory;
    [SerializeField] private Collider mirrorShardCol;

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {        
        if (inventory.ContainsItem(itemName))
        {
            lockReflection.SetActive(true);
            mirrorShardCol.enabled = true;
            interactable = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            this.playerMovement = playerMovement;
            this.playerMovement.CanMove = false;
            cameraControl.SwitchToFixedCamera(cam);
        }
        else
        {
            Debug.Log("I can't see the lock...");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            mirrorShardCol.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            interactable = true;
            playerMovement.CanMove = true;
            cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
        }
    }
}
