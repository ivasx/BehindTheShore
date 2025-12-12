using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnFlashBlink;
    public event Action<float> OnHealthChanged;

    [Header("Movement")]
    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private float minMovingSpeed = 0.1f;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float damageRecoveryTime = 0.5f;
    [SerializeField] private DeathMenu deathMenu;

    [Header("Health Regeneration")]
    [SerializeField] private bool enableRegen = true;
    [SerializeField] private float timeToStartRegen = 3f;
    [SerializeField] private float regenInterval = 1f;
    [SerializeField] private int regenAmount = 1;

    private Vector2 inputVector;
    private Rigidbody2D rb;
    private KnockBack knockBack;

    private bool isRunning = false;
    private int currentHealth;
    private bool canTakeDamage;

    private float lastDamageTime;
    private float regenTimer;

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
        OnHealthChanged?.Invoke(GetHealthNormalized());
        
        if (deathMenu == null)
        {
            deathMenu = FindObjectOfType<DeathMenu>();
        }
    }

    private void Update()
    {
        inputVector = GameInput.Instance.GetMovementVector();
        HandleRegeneration();
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

    public float GetHealthNormalized() 
    {
        return (float)currentHealth / maxHealth;
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
            canTakeDamage = false;
            currentHealth = Mathf.Max(0, currentHealth - damage);
            
            OnHealthChanged?.Invoke(GetHealthNormalized());

            knockBack.GetKnockedBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            
            lastDamageTime = Time.time; 

            StartCoroutine(DamageRecoveryRoutine());
        }
        
        DetectDeath();   
    }

    private void HandleRegeneration()
    {
        if (!enableRegen || currentHealth >= maxHealth || currentHealth <= 0) return;

        if (Time.time >= lastDamageTime + timeToStartRegen)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenInterval)
            {
                currentHealth = Mathf.Min(maxHealth, currentHealth + regenAmount);
                OnHealthChanged?.Invoke(GetHealthNormalized());
                regenTimer = 0f;
            }
        }
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
            if (deathMenu != null)
            {
                deathMenu.PlayerDied();
            }
            
            gameObject.SetActive(false); 
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