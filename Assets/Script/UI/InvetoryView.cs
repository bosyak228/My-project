using System.Collections.Generic;
using UnityEngine;


public class InventoryView : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Transform _rowsContainer;
    [SerializeField] private InventoryRowView _rowPrefab;

    private readonly List<InventoryRowView> _rows = new();

    private void OnEnable()
    {
        _playerInventory.Inventory.Changed += Refresh;
        Refresh(); 
    }

    private void OnDisable() => _playerInventory.Inventory.Changed -= Refresh;

    private void Refresh()
    {

        List<MushroomData> keys = new(_playerInventory.Inventory.Items.Keys);
        keys.Sort((a, b) => a.mushroomName.CompareTo(b.mushroomName));


        for (int i = 0; i < keys.Count; i++)
        {
            InventoryRowView row = i < _rows.Count ? _rows[i] : CreateRow();
            row.Set(keys[i], _playerInventory.Inventory.GetCount(keys[i]));
        }

        for (int i = keys.Count; i < _rows.Count; i++)
            _rows[i].gameObject.SetActive(false);
    }

    private InventoryRowView CreateRow()
    {
        InventoryRowView row = Instantiate(_rowPrefab, _rowsContainer);
        _rows.Add(row);
        return row;
    }
}