using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _balance = 0;

    public void Add(int amount)
    {
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount), "Amount to add cannot be negative.");

        _balance += amount;
    }
}
