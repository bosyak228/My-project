using System.Collections;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] private MushroomData _data;
    [SerializeField] private WorldItem _mushroomPrefab;
    [SerializeField] private float _respawnTime = 60f;

    private WorldItem _currentItem;

    private void Awake() => SpawnItem();


    public void OnItemCollected()
    {
        _currentItem = null;
        StartCoroutine(RespawnRoutine());
    }

    private void SpawnItem()
    {
        Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        _currentItem = Instantiate(_mushroomPrefab, transform.position, rotation, transform);
        _currentItem.Initialize(_data, this);
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_respawnTime);
        SpawnItem();
    }
}