public interface IInteractable
{
    public string InteractionPrompt { get; }

    public bool IsInteractable { get; }

    public bool Interact(Interactor interactor);
}
