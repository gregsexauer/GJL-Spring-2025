using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public bool IsActive { get; private set; } = true;
    public string TooltipText => "Flip";

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite flippedSprite;

    public void Interact()
    {
        spriteRenderer.sprite = flippedSprite;
        IsActive = false;
    }
}
