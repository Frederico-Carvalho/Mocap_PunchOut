using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Health>().TakeDamage(damage);
    }
}