using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private int maxArrows = 6;
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    
    private Animator anim;
    private HealthForOther enemyHealth;
    private Player playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private int currentArrows;
    private bool isReloading;
    [SerializeField] private AudioClip swordSound;
    [SerializeField] private AudioClip arrowSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && coolDownTimer > attackCooldown && playerMovement.CanAttack())
            AttackWithSword();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerMovement.CanAttack() && !isReloading && currentArrows < maxArrows)
                AttackWithArrows();
            else if (currentArrows >= maxArrows && !isReloading)
                StartCoroutine(ReloadArrows());
        }
        coolDownTimer += Time.deltaTime;
    }

    private void AttackWithSword()
    {
        SoundManager.instance.PlaySound(swordSound);
        anim.SetTrigger("Attack");
        coolDownTimer = 0;
        var hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * (range * transform.localScale.x * colliderDistance),
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, enemyLayer);
        if (hit.collider == null) return;
        enemyHealth = hit.transform.GetComponent<HealthForOther>();
        enemyHealth.TakeDamage(damage);
    }
    
    private void AttackWithArrows()
    {
        currentArrows++;
        SoundManager.instance.PlaySound(arrowSound);
        anim.SetTrigger("attackWithBow");
        fireballs[FindArrow()].transform.position = firePoint.position;
        fireballs[FindArrow()].GetComponent<Arrow>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private IEnumerator ReloadArrows()
    {
        isReloading = true;
        yield return new WaitForSeconds(1f);
        currentArrows = 0;
        isReloading = false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private int FindArrow()
    {
        for (var i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}

