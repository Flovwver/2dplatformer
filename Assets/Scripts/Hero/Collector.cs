using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
public class Collector : MonoBehaviour, ICollector
{
    private Health _health;
    private Wallet _wallet;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect(this);
        }
    }

    public void Visit(Coin coin)
    {
        _wallet.Add(coin.Value);

        Destroy(coin.gameObject);
    }

    public void Visit(FirstAidKit kit)
    {
        if (_health.TryHeal(kit.HealAmount))
        {
            Destroy(kit.gameObject);
        }
    }
}
