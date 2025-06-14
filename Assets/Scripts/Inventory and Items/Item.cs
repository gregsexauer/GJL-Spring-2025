using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] Inventory inventory;
    public bool IsActive { get; private set; } = true;
    public string TooltipText { get; private set; } = "Take";

    public void Interact()
    {
        IsActive = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        inventory.PickUpItem(this);
    }
}
