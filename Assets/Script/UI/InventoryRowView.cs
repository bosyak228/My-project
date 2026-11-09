using TMPro;
using UnityEngine;


public class InventoryRowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;

    public void Set(MushroomData data, int count)
    {
        gameObject.SetActive(true);
        _label.text = $"{data.mushroomName} × {count}";
    }
}