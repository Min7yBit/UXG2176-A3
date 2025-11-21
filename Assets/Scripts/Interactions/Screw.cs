using UnityEngine;

public class Screw : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public string itemName;
    [SerializeField]private BedLeg bedLeg;

    private bool interactable = true;
    private bool mouseOver = false;
    [SerializeField] private Inventory inventory;

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
        if (!interactable || !mouseOver)
            return;
        if (inventory.ContainsItem(itemName))
        {
            inventory.RemoveItem(inventory.GetItem(itemName));
            Debug.Log("Screw Removed");
            interactable = false;
            bedLeg.canRemove = true;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Cannot Remove screw, required item not present");
        }
    }
}
