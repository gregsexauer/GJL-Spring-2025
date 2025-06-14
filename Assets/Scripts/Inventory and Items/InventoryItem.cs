using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public Item Item {get; private set;}
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    public void Initialize(GameObject item)
    {
        Item = item.GetComponent<Item>();
        image.sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;
        text.text = item.name;
    }
}
