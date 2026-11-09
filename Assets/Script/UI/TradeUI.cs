using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    [SerializeField] private Transform _rowsContainer;
    [SerializeField] private TradeRowView _rowPrefab;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PlayerInputReader _inputReader;

    private readonly List<TradeRowView> _rows = new();

    private GoblinTrader _trader;
    private PlayerInteractor _interactor;
    private Inventory _inventory;
    private Wallet _wallet;
    private PlayerMovement _movement;
    private PlayerLook _look;
    private bool _isOpen;

    private void Awake()
    {
        _closeButton.onClick.AddListener(Close);
        gameObject.SetActive(false);
    }

    public void Open(GoblinTrader trader, PlayerInteractor interactor)
    {
        if (_isOpen) return;
        _isOpen = true;

        _trader = trader;
        _interactor = interactor;
        _inventory = interactor.Inventory;
        _wallet = interactor.GetComponent<PlayerWallet>().Wallet;
        _movement = interactor.GetComponent<PlayerMovement>();
        _look = interactor.GetComponent<PlayerLook>();


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _movement.enabled = false;
        _look.enabled = false;
        _interactor.enabled = false;

        _inventory.Changed += Refresh;
        _inputReader.Interacted += Close;

        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        if (_interactor == null) return;

        _inventory.Changed -= Refresh;
        _inputReader.Interacted -= Close;

        _movement.enabled = true;
        _look.enabled = true;
        _interactor.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _interactor = null;
        gameObject.SetActive(false);
    }

    private void Refresh()
    {
        List<MushroomData> keys = new(_inventory.Items.Keys);
        keys.Sort((a, b) => a.mushroomName.CompareTo(b.mushroomName));

        for (int i = 0; i < keys.Count; i++)
        {
            TradeRowView row = i < _rows.Count ? _rows[i] : CreateRow();


            MushroomData data = keys[i];
            int count = _inventory.GetCount(data);
            int price = _trader.GetBuyPrice(data);

            row.Set(data, count, price,
                () => _trader.BuyFrom(data, 1, _inventory, _wallet),
                () => _trader.BuyFrom(data, count, _inventory, _wallet));
        }

        for (int i = keys.Count; i < _rows.Count; i++)
            _rows[i].gameObject.SetActive(false);
    }

    private TradeRowView CreateRow()
    {
        TradeRowView row = Instantiate(_rowPrefab, _rowsContainer);
        _rows.Add(row);
        return row;
    }
}