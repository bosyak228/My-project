using System;

public class Wallet
{
    public int Coins { get; private set; }
    public event Action Changed;

    public void Add(int amount)
    {
        if (amount <= 0) return;
        Coins += amount;
        Changed?.Invoke();
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0 || Coins < amount) return false;
        Coins -= amount;
        Changed?.Invoke();
        return true;
    }
}