// using UnityEngine;
//
// public class Mob : MonoBehaviour
// {
//     [Header ("Attack Parameters")]
//     [SerializeField] private float attackCooldown;
//     [SerializeField] private float range;
//     [SerializeField] private int damage;
//
//     [Header("Collider Parameters")]
//     [SerializeField] private float colliderDistance;
//     [SerializeField] private BoxCollider2D boxCollider;
//
//     [Header("Player Layer")]
//     [SerializeField] private LayerMask playerLayer;
//     private Health playerHealth;
//     private float cooldownTimer = Mathf.Infinity;
//
//     private void Update()
//     {
//         cooldownTimer += Time.deltaTime;
//
//         if (!PlayerInSight()) return;
//         if (!(cooldownTimer >= attackCooldown)) return;
//         cooldownTimer = 0;
//         DamagePlayer();
//
//          if (enemyPatrol != null)
//              enemyPatrol.enabled = !PlayerInSight();
//     }
//
//     private bool PlayerInSight()
//     {
//         var hit = 
//             Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
//             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
//             0, Vector2.left, 0, playerLayer);
//
//         if (hit.collider != null)
//         {
//             playerHealth = hit.transform.GetComponent<Health>();
//         }
//
//         return hit.collider != null;
//     }
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
//             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
//     }
//
//     private void DamagePlayer()
//     {
//         if (PlayerInSight())
//             playerHealth.TakeDamage(damage);
//     }
// }