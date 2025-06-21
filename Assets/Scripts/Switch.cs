using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = true;
    public string TooltipText => "Flip";

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite flippedSprite;
    [SerializeField] DangerousWires wires;
    [SerializeField] VendingMachineControls vendingMachineControls;

    public void Interact()
    {
        spriteRenderer.sprite = flippedSprite;
        IsActive = false;
        wires.TurnOffElectricity();
        vendingMachineControls.PowerOff();
    }
}
