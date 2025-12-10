using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private bool alwaysEnableCollider = false;
    
    private int currentHealth;
    private PolygonCollider2D attackCollider;
    
    public event EventHandler OnTakeHit;

    private void Awake()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
    }
    
    private void Start()
    {
        if (enemySO != null)
        {
            currentHealth = enemySO.enemyHealth;
        }
        else
        {
            Debug.LogError("EnemySO not assigned in EnemyEntity on " + gameObject.name);
            currentHealth = 10;
        }

        if (attackCollider != null)
        {
            attackCollider.enabled = alwaysEnableCollider;
            attackCollider.isTrigger = true; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (enemySO == null) return;

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
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}");
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} died");
            Destroy(gameObject);
        }
    }
}