public interface IInteractable
{
    public bool IsActive { get; }
    public string TooltipText { get; }

    public void Interact();
}
