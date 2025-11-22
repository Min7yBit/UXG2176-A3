using UnityEngine;

public class BrokenWall : MonoBehaviour, IInteractable
{
    public string Name => name;
    public bool canInteract { get => interactable; set { interactable = value; } }

    [Header("Requirements")]
    [Tooltip("Name of the item needed to scrape the wall.")]
    public string requiredItemName = "Spoon";

    [Header("Diggy hole Settings")]
    [Tooltip("How many times the player must scrape before the hint appears.")]
    public int scrapesNeeded = 3;

    [Header("Wall & Hint Objects")]
    [Tooltip("Intact wall mesh/obj to hide after scraping is done.")]
    public GameObject intactWallObject;
    [Tooltip("Scraped/broken version of the wall to show when solved.")]
    public GameObject scrapedWallObject;
    [Tooltip("The hint object")]
    public GameObject hint2Object;

    [Header("References")]
    [SerializeField] private Inventory inventory;   

    private bool interactable = false;
    private bool mouseOver = false;
    private int currentScrapes = 0;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

       
        if (hint2Object != null) hint2Object.SetActive(false);
        if (scrapedWallObject != null) scrapedWallObject.SetActive(false);
        if (intactWallObject != null) intactWallObject.SetActive(true);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        interactable = true;

        // Optional highlight
        if (rend != null)
        {
            Color c = rend.material.color;
            c.a = 0.6f;
            rend.material.color = c;
        }
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        interactable = false;

        // Reset highlight
        if (rend != null)
        {
            Color c = rend.material.color;
            c.a = 1f;
            rend.material.color = c;
        }
    }

    public void OnInteract(in PlayerMovement playerMovement)
    {
        // Must be the focused interactable AND currently interactable
        if (!interactable || !mouseOver)
            return;

        Debug.Log($"Attempting to scrape wall with {requiredItemName}");

        if (inventory == null)
        {
            Debug.LogWarning("BrokenWall: Inventory reference is missing!");
            return;
        }

        // Your Inventory.ContainsItem() only returns true if that item is in a SELECTED slot.
        if (!inventory.ContainsItem(requiredItemName))
        {
            Debug.Log("The wall looks weak... Maybe I can use something to scrape it.");
            // Here you can hook up UI feedback instead of Debug.Log
            return;
        }

        // Player has the spoon selected ? scrape
        currentScrapes++;
        Debug.Log($"Scraping wall... ({currentScrapes}/{scrapesNeeded})");

        // Optional: add scrape SFX or particle effect here

        if (currentScrapes >= scrapesNeeded)
        {
            RevealHint();
        }
    }

    private void RevealHint()
    {
        Debug.Log("The plaster flakes away... You found Hint 2!");

        if (intactWallObject != null)
            intactWallObject.SetActive(false);

        if (scrapedWallObject != null)
            scrapedWallObject.SetActive(true);

        if (hint2Object != null)
            hint2Object.SetActive(true);

        // Stop further interactions
        interactable = false;
        mouseOver = false;
    }
}
