public interface IInteractable
{
    string InteractionHint { get; }
    void Interact(PlayerInteractor interactor);
}