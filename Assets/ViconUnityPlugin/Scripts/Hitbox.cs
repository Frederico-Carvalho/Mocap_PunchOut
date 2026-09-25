using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float speedToDamage = 0.3f;
    public float minDamage = 0.1f;
    public float maxDamage = 5f;
  
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        currentSpeed = speed;
        lastPosition = transform.position;
    }

    float currentSpeed;

    void OnTriggerEnter(Collider other)
    {
        float baseDamage = Mathf.Clamp(currentSpeed * speedToDamage, minDamage, maxDamage);

        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        Health health = other.GetComponentInParent<Health>();
        if (hurtbox != null && health != null)
        {
            health.TakeDamage(baseDamage * hurtbox.damageMultiplier);
        }

        HitFeedback feedback = other.GetComponentInParent<HitFeedback>();
        if (feedback != null)
        {
            feedback.TriggerHit();
        }
    }
}