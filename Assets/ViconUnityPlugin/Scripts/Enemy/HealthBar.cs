using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Image fillImage;
    public float maxHealth = 100f;

    void Update()
    {
        if (health.current <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        fillImage.fillAmount = health.current / maxHealth;
    }
}
