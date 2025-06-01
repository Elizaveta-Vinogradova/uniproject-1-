using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class NPC_patrol : MonoBehaviour
{
    public float speed = 2;
    public float pauseDuration = 1.5f;
    public Vector2[] patrolPoints;
    private Vector2 target;
    private int currentPatrolIndex;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(SetPatrolPoints());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (NPC.playerIsClose)
        {
            anim.Play("wt_idle");
        }
        
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        if (direction.x > 0 && transform.localScale.x < 0 || direction.x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.linearVelocity = direction * speed;
        
        if(Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(SetPatrolPoints());
    }

    IEnumerator SetPatrolPoints()
    {
        isPaused = true;
        anim.Play("wt_idle");
        yield return new WaitForSeconds(pauseDuration);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPaused = false;
        anim.Play("wt_run");
    }
}
