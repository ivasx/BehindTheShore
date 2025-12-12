using UnityEngine;

public class RunesClaim : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || other.GetComponent<Player>() == null) return;

        isCollected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RuneCollected();
        }

        Destroy(gameObject);
    }
}