using UnityEngine;

public class CombinableItem : Item, ICombinable
{
    public bool AddedToCombine { get; set; }
    public void Combine(ICombinable otherItem)
    {

    }
}
