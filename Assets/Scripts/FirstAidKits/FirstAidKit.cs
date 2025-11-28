using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FirstAidKit : MonoBehaviour, ICollectible
{
    [SerializeField] private int _healAmount = 2;

    public int HealAmount => _healAmount;

    public void Collect(Collector collector) => collector.Visit(this);
}
