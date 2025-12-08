using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnFlashBlink;

    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float damageRecoveryTime = 0.5f;

    private Vector2 inputVector;
    private Rigidbody2D rb;
    private KnockBack knockBack;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    private int currentHealth;
    private bool canTakeDamage;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        canTakeDamage = true;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate()
    {
        if (knockBack.IsGettingKnockedBack)
            return;

        HandleMovement();
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (canTakeDamage)
        {
            Debug.Log("Current health: " + currentHealth + "");
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth -= damage);
            knockBack.GetKnockedBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DamageRecoveryRoutine());
        }
        
        DetectDeath();   
        
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }        
    }
    
    private void HandleMovement()
    {
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Math.Abs(inputVector.x) > minMovingSpeed || Math.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
}