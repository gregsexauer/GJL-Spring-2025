using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemPrefab;
    List<Item> _items = new();
    List<InventoryItem> _inventoryItems = new();

    public void PickUpItem(Item item)
    {
        _items.Add(item);
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, this.transform);
        inventoryItem.GetComponent<InventoryItem>().Initialize(item.gameObject);
    }

    public void RemoveItem(Item item)
    {
        _items.Remove(item);
        foreach (InventoryItem inventoryItem in _inventoryItems)
        {
            if (inventoryItem.Item == item)
            {
                _inventoryItems.Remove(inventoryItem);
                Destroy(inventoryItem.gameObject);
            }
        }
    }
}
