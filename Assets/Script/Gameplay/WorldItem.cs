using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    private MushroomData _data;
    private MushroomSpawner _spawner;

    public string InteractionHint => $"Собрать: {_data.mushroomName}";

    // Точка-грибница конфигурирует проявление при рождении
    public void Initialize(MushroomData data, MushroomSpawner spawner)
    {
        _data = data;
        _spawner = spawner;
    }

    public void Interact(PlayerInteractor interactor)
    {
        interactor.Inventory.Add(_data);
        _spawner?.OnItemCollected();
        Destroy(gameObject);
    }
}