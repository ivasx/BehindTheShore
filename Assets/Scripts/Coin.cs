using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Перевіряємо, чи це гравець
        if (other.GetComponent<Player>() != null)
        {
            // Кажемо менеджеру зарахувати монету
            GameManager.Instance.CoinCollected();
            
            // Знищуємо саму монетку
            Destroy(gameObject);
        }
    }
}