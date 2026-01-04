using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.Instance != null)
                CoinManager.Instance.AddCoin(value);

            Destroy(gameObject);
        }
    }
}
