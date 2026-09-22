using UnityEngine;

public class Health : MonoBehaviour
{
    public int current = 100;

    public void TakeDamage(int amount)
    {
        current -= amount;
    }
}