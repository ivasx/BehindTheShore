using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<Player>() != null)
        {

            GameManager.Instance.CoinCollected();
            

            Destroy(gameObject);
        }
    }
}