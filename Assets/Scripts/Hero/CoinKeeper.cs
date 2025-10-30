using UnityEngine;

public class CoinKeeper : MonoBehaviour
{
    [field: SerializeField]
    public int CoinValue { get; private set; } = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Coin>(out var _))
        {
            Destroy(collider.gameObject);
            CoinValue++;
        }
    }
}
