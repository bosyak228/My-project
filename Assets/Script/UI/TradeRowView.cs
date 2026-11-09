using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeRowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Button _sellOneButton;
    [SerializeField] private Button _sellAllButton;

    public void Set(MushroomData data, int count, int price, Action onSellOne, Action onSellAll)
    {
        gameObject.SetActive(true);
        _label.text = $"{data.mushroomName} × {count} — {price} монет/шт";

        _sellOneButton.onClick.RemoveAllListeners();
        _sellAllButton.onClick.RemoveAllListeners();
        _sellOneButton.onClick.AddListener(() => onSellOne());
        _sellAllButton.onClick.AddListener(() => onSellAll());
    }
}