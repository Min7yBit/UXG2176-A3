using UnityEngine;

public class Mirror : MonoBehaviour, IInteractable
{
    public string Name => name;

    public bool canInteract { get => interactable; set { interactable = value; } }

    public string itemName;
    [SerializeField] private Item shardItem;
    [SerializeField] private GameObject mirrorMess;
    [SerializeField] private Inventory inventory;
    [SerializeField] private bool interactable;
    //private Renderer Rrenderer;
    private bool mouseOver = false;
    private void Awake()
    {
        //Rrenderer = GetComponent<Renderer>();
    }
    public Transform GetTransform()
    {
        return transform;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered " + name);
        //Rrenderer.material.color = Color.yellow;
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited " + name);
        //Rrenderer.material.color = Color.white;
        mouseOver = false;
    }
    public void OnInteract(in PlayerMovement playerMovement)
    {
        Debug.Log("Attempting to interact with " + name);
        if (!mouseOver)
            return;

        Debug.Log("Interacted with " + name);

        if (inventory.ContainsItem(itemName) && shardItem != null) //make sure bedleg item is in inventory
        {
            //Koon:can add sfx here
            inventory.RemoveItem(inventory.GetItem(itemName)); //remove bed leg item from inventory
            playerMovement.GetComponent<Inventory>().AddItem(shardItem); //testing adding item to inventory on interact, some items may not have Item component
            interactable = false;
            mirrorMess.SetActive(true); //show the mirror mess
            gameObject.SetActive(false); //disable the object after picking it up
            Debug.Log("Mirror Broken");
        }
        else
        {
            Debug.Log("Hmm maybe I can break this with something.");
        }
    }
}
