using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;

    public void AddItem(Item itemtoAdd)
    {
        //is the item a key?
        if(itemtoAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if(!items.Contains(itemtoAdd))
            {
                items.Add(itemtoAdd);
            }
        }
    }
}
