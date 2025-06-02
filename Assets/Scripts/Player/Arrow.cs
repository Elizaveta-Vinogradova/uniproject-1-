using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private readonly int damage = 1;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        var movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CoffeeGrain")) return;
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        if (!collision.CompareTag("Enemy")) return;
        collision.GetComponent<HealthForOther>().TakeDamage(damage);
    }
    public void SetDirection(float directionArrow)
    {
        lifetime = 0;
        direction = directionArrow;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        var localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != directionArrow)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate() => gameObject.SetActive(false);
}