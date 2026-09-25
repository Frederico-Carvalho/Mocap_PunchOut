using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        HitFeedback feedback = other.GetComponentInParent<HitFeedback>();
        if (feedback != null)
        {
            feedback.TriggerHit();
        }
        Destroy(gameObject);
    }
}
