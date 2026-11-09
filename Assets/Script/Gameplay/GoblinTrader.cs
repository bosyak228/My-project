using UnityEngine;


public class GoblinTrader : MonoBehaviour, IInteractable
{
    [SerializeField] private TradeUI _tradeUI;
    [SerializeField] private float _priceMultiplier = 1f;

    public string InteractionHint => "Торговать с Гоблусси";

    public int GetBuyPrice(MushroomData data) =>
        Mathf.RoundToInt(data.price * _priceMultiplier);

    public void Interact(PlayerInteractor interactor)
    {
        _tradeUI.Open(this, interactor);
    }

    public bool BuyFrom(MushroomData data, int count, Inventory inventory, Wallet wallet)
    {
        if (!inventory.Remove(data, count)) return false;

        wallet.Add(GetBuyPrice(data) * count);
        return true;
    }
}