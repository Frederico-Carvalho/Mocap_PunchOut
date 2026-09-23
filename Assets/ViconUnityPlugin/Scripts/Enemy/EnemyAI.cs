using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    [SerializeField] string[] comboTriggers;
    [SerializeField] string rangedAttackTrigger;
    public float closeRange = 3f;
    public float farRange = 10f;
    public float comboCooldown = 2f;
    public float rangedCooldown = 3f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;

    float comboTimer;
    float rangedTimer;
    bool rangedAttackPlaying;

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        transform.rotation = Quaternion.LookRotation(toPlayer);

        float distance = toPlayer.magnitude;
        comboTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;

        if (distance <= closeRange && comboTimer <= 0f)
        {
            animator.SetTrigger(comboTriggers[Random.Range(0, comboTriggers.Length)]);
            comboTimer = comboCooldown;
        }
        else if (distance > closeRange && distance <= farRange && rangedTimer <= 0f && !rangedAttackPlaying)
        {
            animator.SetTrigger(rangedAttackTrigger);
            rangedAttackPlaying = true;
            rangedTimer = rangedCooldown;
        }
    }

    // Chamado por um Animation Event no clip de mocap do ataque à distância
    public void SpawnProjectile()
    {
        Vector3 direction = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        proj.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;
        rangedAttackPlaying = false;
    }
}