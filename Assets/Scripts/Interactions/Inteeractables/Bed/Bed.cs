using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour,IInteractable

{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;
    public CameraControl cameraControl;
    public Camera cam;

    private bool interactable = true;
    private PlayerMovement playerMovement;
    [SerializeField] private List<Transform> childTransforms;

    private void Awake()
    {
        //Rrenderer = GetComponent<Renderer>();
        InitiateTransformList();
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        InInteract = true;
        EnableChildInteraction();
        interactable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.playerMovement = playerMovement;
        this.playerMovement.CanMove = false;
        cameraControl.SwitchToFixedCamera(cam);
    }

    private void Update()
    {
        if (InInteract)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                InInteract = false;
                DisableChildInteraction();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                interactable = true;
                playerMovement.CanMove = true;
                cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
            }
        }
    }
    private void InitiateTransformList()
    {
        childTransforms = new List<Transform>();
        Transform[] newTransformList = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < newTransformList.Length; i++) //start at 1 to skip the parent transform
        {
            childTransforms.Add(newTransformList[i]);
        }

        DisableChildInteraction();
    }
    private void DisableChildInteraction()
    {
        foreach (Transform child in childTransforms)
        {
            IInteractable interactableComponent = child.GetComponent<IInteractable>();
            if (interactableComponent != null)
            {
                interactableComponent.CanInteract = false;
            }
        }
    }

    private void EnableChildInteraction()
    {
        foreach (Transform child in childTransforms)
        {
            IInteractable interactableComponent = child.GetComponent<IInteractable>();
            if (interactableComponent != null)
            {
                interactableComponent.CanInteract = true;
            }
        }
    }
}
