using System.Collections;
using TMPro;
using UnityEngine;

public class Screw : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;
    public bool ShowPrompt { get; set; } = true;
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
                ShowMessage("Cannot remove Screw, required item not present");
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

    public TextMeshProUGUI messageText;

    public void ShowMessage(string msg, float duration = 2f)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        StartCoroutine(FadeMessage(duration));
    }

    private IEnumerator FadeMessage(float duration)
    {
        // Reset alpha to fully visible
        Color c = messageText.color;
        c.a = 1;
        messageText.color = c;

        // Fade out over time
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, elapsed / duration);
            messageText.color = c;
            yield return null;
        }

        // Hide once fully faded
        messageText.gameObject.SetActive(false);
    }
}
