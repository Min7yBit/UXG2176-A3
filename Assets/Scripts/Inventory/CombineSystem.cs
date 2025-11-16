using System.Collections.Generic;
using UnityEngine;

public class CombineSystem : MonoBehaviour
{
    // This is the item to be combined
    private Item itemA;
    private Item itemB;
    private Item combinedItem;

    //to be filled up in inspector
    [SerializeField] private List<ItemRecipe> itemRecipes;

    // UI Elements
    public GameObject itemSlotA;
    public GameObject itemSlotB;
    public GameObject combinedSlot;

    public void AddToCombineSlot(Item item)
    {
        if (itemA == null)
        {
            itemA = item;
        }
        else if (itemB == null)
        {
            itemB = item;
        }
    }

    private void DisplayItemUI()
    {
        if (itemA != null)
        {
            itemSlotA.GetComponent<UnityEngine.UI.Image>().sprite = itemA.icon;
        }
        if (itemB != null)
        {
            itemSlotB.GetComponent<UnityEngine.UI.Image>().sprite = itemB.icon;
        }
        if (combinedItem != null)
        {
            combinedSlot.GetComponent<UnityEngine.UI.Image>().sprite = combinedItem.icon;
        }
    }
}

[System.Serializable]
public class ItemRecipe
{
    public Item inputA;
    public Item inputB;
    public Item result;
}