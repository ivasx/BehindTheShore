using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private void Start()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnHealthChanged += UpdateVisual;
            UpdateVisual(Player.Instance.GetHealthNormalized());
        }
        else
        {
            Debug.LogError("Player Instance is null!");
        }
    }

    private void UpdateVisual(float healthNormalized)
    {
        healthBarImage.fillAmount = healthNormalized;
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnHealthChanged -= UpdateVisual;
        }
    }
}