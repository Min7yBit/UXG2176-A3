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

    public void OnInteract()
    {
        Debug.Log("Interacted with " + name);
    }


}
