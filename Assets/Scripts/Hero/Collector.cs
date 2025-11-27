using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(out ICollectible collectible))
        {
            collectible.Collect(gameObject);
        }
    }
}
