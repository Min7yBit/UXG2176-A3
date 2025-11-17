using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract => interactable;

    public string itemName;

    private bool interactable = true;
    [SerializeField]private Inventory inventory;

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        
        if (inventory.ContainsItem(itemName))
        {
            Debug.Log("Open Door");
            interactable = false;
        }
        else
        {
            Debug.Log("Cannot Open door, required item not present");
        }
    }

    private void Update()
    {

    }
}
