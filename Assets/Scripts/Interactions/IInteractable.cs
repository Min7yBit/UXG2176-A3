using UnityEngine;

public interface IInteractable
{
    public string Name { get;}

    public bool canInteract {  get;}
    public void OnInteract();
    public Transform GetTransform();
}
