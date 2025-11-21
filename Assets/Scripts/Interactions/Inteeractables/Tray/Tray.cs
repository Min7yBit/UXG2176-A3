using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract  { get => interactable; set { interactable = value; } }

    public CameraControl cameraControl;
    public Camera cam;
    public Potato potato;

    /*    private Renderer Rrenderer;
        private bool mouseOver = false;*/
    private bool interactable = true;
    private PlayerMovement playerMovement;
    [SerializeField]private List<Transform> childTransforms;

    private void Awake()
    {
        //Rrenderer = GetComponent<Renderer>();
        InitiateTransformList();
    }

    public Transform GetTransform()
    {
        return transform;
    }
/*    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Tray");
        Rrenderer.material.color = Color.yellow;
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Tray");
        Rrenderer.material.color = Color.white;
        mouseOver = false;
    }*/
    public void OnInteract(in PlayerMovement playerMovement)
    {
/*        if (!mouseOver)
            return;*/
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            potato.ResetPotato();
            DisableChildInteraction();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            interactable = true;
            playerMovement.CanMove = true;
            cameraControl.SetCameraMode(CameraControl.CameraMode.ThirdPerson);
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
                interactableComponent.canInteract = false;
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
                interactableComponent.canInteract = true;
            }
        }
    }
}
