using System;
using UnityEngine;


public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    
    private int currentHealth;

    private PolygonCollider2D attackCollider;
    public event EventHandler OnTakeHit;
    private void Awake()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        if (attackCollider == null)
        {
            Debug.LogError("PolygonCollider2D not found to " + gameObject.name);
        }
    }
    
    private void Start()
    {
        currentHealth = enemySO.enemyHealth;
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
            attackCollider.isTrigger = true; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, enemySO.enemyDamageAmount);
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
        OnTakeHit?.Invoke(this, System.EventArgs.Empty);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " is dead!");
            Destroy(gameObject);
        }
    }
}