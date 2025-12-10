using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damagableObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;

    private float blinkTimer;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("FlashBlink requires SpriteRenderer on the same GameObject!");
            enabled = false;
            return;
        }
        
        defaultMaterial = spriteRenderer.material;
    }

    private void Start()
    {
        if (damagableObject == null)
        {
            Debug.LogError("Damagable Object not assigned in FlashBlink on " + gameObject.name);
            return;
        }
        
        if (damagableObject is Player player)
        {
            player.OnFlashBlink += DamagableObject_OnFlashBlink;
            Debug.Log("FlashBlink subscribed to Player");
        }
        else if (damagableObject is EnemyEntity enemy)
        {
            enemy.OnTakeHit += DamagableObject_OnFlashBlink;
            Debug.Log("FlashBlink subscribed to EnemyEntity: " + gameObject.name);
        }
    }

    private void DamagableObject_OnFlashBlink(object sender, System.EventArgs e)
    {
        Debug.Log("Flash blink triggered on " + gameObject.name);
        SetBlinkingMaterial();
    }

    private void Update()
    {
        if (isBlinking && blinkTimer > 0)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {
                SetDefaultMaterial();
            }
        }
    }

    private void SetBlinkingMaterial()
    {
        if (blinkMaterial == null)
        {
            Debug.LogError("Blink Material not assigned!");
            return;
        }
        
        blinkTimer = blinkDuration;
        spriteRenderer.material = blinkMaterial;
    }

    private void SetDefaultMaterial()
    {
        spriteRenderer.material = defaultMaterial;
    }

    public void StopBlinking()
    {
        SetDefaultMaterial();
        isBlinking = false;
    }

    private void OnDestroy()
    {
        if (damagableObject is Player player)
        {
            player.OnFlashBlink -= DamagableObject_OnFlashBlink;
        }
        else if (damagableObject is EnemyEntity enemy)
        {
            enemy.OnTakeHit -= DamagableObject_OnFlashBlink;
        }
    }
}