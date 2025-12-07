using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private PolygonCollider2D polygonCollider2D;

    private void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attacked");
    }
    
    public void PolygonCollider2DTurnOff()
    {
        polygonCollider2D.enabled = false;
    }

    public void PolygonCollider2DTurnOn()
    {
        polygonCollider2D.enabled = true;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}