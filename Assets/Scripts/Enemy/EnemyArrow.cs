using UnityEngine;

public class EnemyArrow : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        var movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
    
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CoffeeGrain")) return;
        hit = true;
        base.OnTriggerEnter2D(collision);
        coll.enabled = false;
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
        if (anim != null)
            anim.SetTrigger("explode"); 
        else
            gameObject.SetActive(false); 
    }
    private void Deactivate() => gameObject.SetActive(false);
}