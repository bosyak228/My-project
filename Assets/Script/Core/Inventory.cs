using System;
using System.Collections.Generic;

public class Inventory
{
    private readonly Dictionary<MushroomData, int> _items = new();

    public event Action Changed;

    public IReadOnlyDictionary<MushroomData, int> Items => _items;

    public void Add(MushroomData item, int count = 1)
    {
        if (item == null || count <= 0) return;

        _items.TryGetValue(item, out int current);
        _items[item] = current + count;
        Changed?.Invoke();
    }

    public bool Remove(MushroomData item, int count = 1)
    {
        if (item == null || !_items.TryGetValue(item, out int current) || current < count)
            return false;

        current -= count;
        if (current == 0) _items.Remove(item);
        else _items[item] = current;

        Changed?.Invoke();
        return true;
    }

    public int GetCount(MushroomData item) =>
        item != null && _items.TryGetValue(item, out int current) ? current : 0;
}