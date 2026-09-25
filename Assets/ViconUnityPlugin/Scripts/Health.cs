using UnityEngine;

public class Health : MonoBehaviour
{
    public float current = 100;

    public void TakeDamage(float amount)
    {
        if (current <= 0) return;
        current -= amount;
        if (current <= 0)
        {
            GetComponent<RagdollSwap>().TriggerRagdoll();
        }
    }
}