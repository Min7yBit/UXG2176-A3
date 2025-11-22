using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

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
            InInteract = true;
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
        if (InInteract)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                InInteract = false;
                mirrorShardCol.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                interactable = true;
                playerMovement.CanMove = true;
                cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
            }
        }
    }
}
