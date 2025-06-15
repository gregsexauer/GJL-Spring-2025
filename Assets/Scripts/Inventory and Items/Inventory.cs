using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemPrefab;
    static List<Item> _items = new();
    static List<InventoryItem> _inventoryItems = new();

    public void PickUpItem(Item item)
    {
        _items.Add(item);
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, this.transform);
        inventoryItem.GetComponent<InventoryItem>().Initialize(item.gameObject);
        _inventoryItems.Add(inventoryItem.GetComponent<InventoryItem>());
    }

    [YarnCommand("Remove_Item")]
    public static void RemoveItem(string itemName)
    {
        foreach (Item item in _items)
        {
            if (item.gameObject.name == itemName)
            {
                _items.Remove(item);
                break;
            }
        }

        foreach (InventoryItem inventoryItem in _inventoryItems)
        {
            if (inventoryItem.Item.gameObject.name == itemName)
            {
                _inventoryItems.Remove(inventoryItem);
                Destroy(inventoryItem.gameObject);
                break;
            }
        }
    }

    [YarnFunction("Does_Inventory_Contain_Item")]
    public static bool DoesInventoryContainItem(string itemName)
    {
        bool itemFound = false;
        foreach (Item item in _items)
            if (item.gameObject.name == itemName)
                itemFound = true;

        return itemFound;
    }
}
