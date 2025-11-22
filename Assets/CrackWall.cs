using UnityEngine;

public class CrackWall : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool CanInteract { get => interactable; set { interactable = value; } }
    public bool InInteract { get; set; } = false;

    [Header("Required Item")]
    [Tooltip("Name of item required to scrape the wall.")]
    public string itemName = "Spoon";

    [Header("Scrape Settings")]
    [Tooltip("How many diggity digs (clicks) before the wall breaks.")]
    public int scrapesNeeded = 3;
    private int currentScrapes = 0;

    [Header("Cell / Wall Objects")]
    [Tooltip("The default ver.")]
    [SerializeField] private GameObject intactCell;
    [Tooltip("The hole ver")]
    [SerializeField] private GameObject holedCell;
    [Tooltip("Hint object to reveal after wall is broken.")]
    [SerializeField] private GameObject hintObject;

    [Header("Assing Player Inventory")]
    [SerializeField] private Inventory inventory;

    private bool interactable = true;

    public Transform GetTransform()
    {
        return transform;
    }

    private void Awake()
    {
        // Ensure starting state is correct
        if (intactCell != null) intactCell.SetActive(true);
        if (holedCell != null) holedCell.SetActive(false);
        if (hintObject != null) hintObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered CrackWall area " );
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited CrackWall area " );
    }

    private void OnMouseOver()
    {
        if (!interactable)
            return;

        // Left click while hovering
        if (Input.GetMouseButtonDown(0))
        {
            if (inventory == null)
            {
                Debug.LogWarning("CrackWall: Inventory reference not assigned in Inspector.");
                return;
            }

            // Your Inventory.ContainsItem only returns true if that item is in a SELECTED slot.
            if (inventory.ContainsItem(itemName))
            {
                currentScrapes++;
                Debug.Log($"Scraping wall with {itemName}... ({currentScrapes}/{scrapesNeeded})");

                if (currentScrapes >= scrapesNeeded)
                {
                    RevealHint();
                }
            }
            else
            {
                Debug.Log($"Cannot scrape wall, required item not selected: {itemName}");
            }
        }
    }

    private void RevealHint()
    {
        Debug.Log("Wall broken, revealing hole and Hint.");

        interactable = false;

        if (intactCell != null)
            intactCell.SetActive(false);

        if (holedCell != null)
            holedCell.SetActive(true);

        if (hintObject != null)
            hintObject.SetActive(true);

       
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
       
    }
}
