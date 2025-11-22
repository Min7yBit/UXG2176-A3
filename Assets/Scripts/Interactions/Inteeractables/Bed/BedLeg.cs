using Unity.VisualScripting;
using UnityEngine;

public class BedLeg : MonoBehaviour, IInteractable
{
    public string Name => name;

    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

    public bool canRemove = false;
    
    private bool interactable;
    private Renderer Rrenderer;
    private bool mouseOver = false;
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
        if (!interactable   )
            return;
        Debug.Log("Mouse Entered " + name);
        Rrenderer.material.color = Color.yellow;
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        if (!interactable)
            return;
        Debug.Log("Mouse Exited " + name);
        Rrenderer.material.color = Color.white;
        mouseOver = false;
    }

    private void OnMouseOver()
    {
        if (!interactable)
            return;
        if (Input.GetMouseButton(0))
        {

            if (!canRemove)
            {
                Debug.Log("I can't remove it with the screw on.");
                return;
            }

            Debug.Log("Interacted with " + name);
            Item item = GetComponent<Item>();
            if (item != null)
            {
                //playerMovement.GetComponent<Inventory>().AddItem(item); //testing adding item to inventory on interact, some items may not have Item component
                gameObject.SetActive(false); //disable the object after picking it up
            }
        }

    }
    public void OnInteract(in PlayerMovement playerMovement)
    {
        
    }
}
