using UnityEngine;
using System.Collections;

public class BedMattress : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

    private Renderer Rrenderer;
    private bool mouseOver = false;
    private bool interactable = true;
    private void Awake()
    {
        Rrenderer = GetComponent<Renderer>();
    }
    public Transform GetTransform()
    {
        return transform;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Mattress");
        Rrenderer.material.color = Color.yellow;
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Mattress");
        Rrenderer.material.color = Color.white;
        mouseOver = false;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        if (!interactable || !mouseOver)
            return;

        //display message on UI
        Debug.Log("I don't wanna sleep yet.");

    }
}
