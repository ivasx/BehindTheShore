using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private PolygonCollider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        if (attackCollider == null)
        {
            Debug.LogError("PolygonCollider2D не знайдено на " + gameObject.name);
        }
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
            attackCollider.isTrigger = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Attacked Player!");
            
        }
        else
        {
            Debug.Log("Attacked: " + other.gameObject.name);
        }
    }
    
    public void PolygonCollider2DTurnOff()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
    }

    public void PolygonCollider2DTurnOn()
    {
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " отримав " + damage + " пошкоджень. Залишилось HP: " + currentHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " помер!");
            Destroy(gameObject);
        }
    }
}