using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private int _value = 1;

    public int Value => _value;

    public void Collect(Collector collector) => collector.Visit(this);

}
