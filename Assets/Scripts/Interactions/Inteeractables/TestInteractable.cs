using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string Name => name;

    public bool canInteract { get => interactable; set { interactable = value; } }

    public bool interactable;    

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        if (!interactable)
            return;
        Debug.Log("Interacted with " + name);

        Item item = GetComponent<Item>();
        if (item != null)
        {
            playerMovement.GetComponent<Inventory>().AddItem(item); //testing adding item to inventory on interact, some items may not have Item component
            gameObject.SetActive(false); //disable the object after picking it up
        }
    }
}
