using UnityEngine;

public class MobKnifer : MonoBehaviour
{
    [Header("Attack Parameters")] [SerializeField]
    private float attackCooldown;

    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")] [SerializeField]
    private float colliderDistance;

    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")] [SerializeField]
    private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    [Header("AttackSound")] [SerializeField]
    private AudioClip attackSound;

    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            if (cooldownTimer >= attackCooldown)
            {
                DamagePlayer();
                anim.SetTrigger("attack");
            }
        }

        if (enemyPatrol)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        var hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        SoundManager.instance.PlaySound(attackSound);
        cooldownTimer = 0;
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}