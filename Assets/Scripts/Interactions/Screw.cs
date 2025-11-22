using UnityEngine;

public class Screw : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    public string itemName;
    [SerializeField]private BedLeg bedLeg;

    private bool interactable = true;
    [SerializeField] private Inventory inventory;

    public Transform GetTransform()
    {
        return transform;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered Bed Clickable Area " + name);
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited Bed Clickable Area " + name );
    }

    private void OnMouseOver()
    {
        if (!interactable)
            return;

        if (Input.GetMouseButton(0))
        {

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

    public void OnInteract(in PlayerMovement playerMovement)
    {
        //if (!interactable || !mouseOver)
        //    return;
        //if (inventory.ContainsItem(itemName))
        //{
        //    inventory.RemoveItem(inventory.GetItem(itemName));
        //    Debug.Log("Screw Removed");
        //    interactable = false;
        //    bedLeg.canRemove = true;
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    Debug.Log("Cannot Remove screw, required item not present");
        //}
    }
}
