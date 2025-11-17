using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _healAmount = 2;

    public int HealAmount => _healAmount;
}
