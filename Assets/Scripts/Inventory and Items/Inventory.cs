using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemPrefab;
    static List<Item> _items = new();
    static List<InventoryItem> _inventoryItems = new();

    private void Awake()
    {
        foreach (Item item in _items)
        {
            if (item != null)
                RemoveItem(item.name);
        }
        _items.Clear();
        _inventoryItems.Clear();
    }

    public void PickUpItem(Item item)
    {
        _items.Add(item);
        GameObject inventoryItem = Instantiate(inventoryItemPrefab, this.transform);
        inventoryItem.GetComponent<InventoryItem>().Initialize(item.gameObject);
        _inventoryItems.Add(inventoryItem.GetComponent<InventoryItem>());
    }

    public void Remove(string itemName)
    {
        RemoveItem(itemName);
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

    public bool DoesContainItem(string item)
    {
        return DoesInventoryContainItem(item);
    }

    [YarnFunction("Does_Inventory_Contain_Item")]
    public static bool DoesInventoryContainItem(string itemName)
    {
        bool itemFound = false;

        foreach (Item item in _items)
            if (item != null && item.gameObject.name == itemName)
                itemFound = true;

        return itemFound;
    }
}
