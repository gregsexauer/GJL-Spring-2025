using UnityEngine;

public class CoinInsert : MonoBehaviour, IInteractable
{
    [SerializeField] Inventory inventory;
    [SerializeField] VendingMachineControls controls;
    public bool IsActive { get; private set; } = false;
    public string TooltipText { get; private set; } = "Insert";

    void Update()
    {
        if (inventory.DoesContainItem("Some Change"))
        {
            IsActive = true;
        }
    }

    public void Interact()
    {
        inventory.Remove("Some Change");
        controls.OnInsertCoins();
        IsActive = false;
    }
}
