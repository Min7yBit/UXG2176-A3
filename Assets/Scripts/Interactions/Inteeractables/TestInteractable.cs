using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string Name => name;

    public bool canInteract => interact;

    public bool interact;

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        Debug.Log("Interacted with " + name);

        Item item = GetComponent<Item>();
        if (item != null)
            playerMovement.GetComponent<Inventory>().AddItem(item); //testing adding item to inventory on interact, some items may not have Item component
    }


}
