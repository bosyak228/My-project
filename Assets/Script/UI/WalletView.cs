using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private TextMeshProUGUI _label;

    private void OnEnable()
    {
        _playerWallet.Wallet.Changed += Refresh;
        Refresh();
    }

    private void OnDisable() => _playerWallet.Wallet.Changed -= Refresh;

    private void Refresh() => _label.text = $"Монеты: {_playerWallet.Wallet.Coins}";
}