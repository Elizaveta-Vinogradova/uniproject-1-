using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallDelay = 1.0f; 
    public float fallSpeed = 5.0f; 
    public float destroyDelay = 3.0f; 
    
    private Rigidbody2D rb;
    private bool isFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || isFalling) return;
        isFalling = true;
        Invoke("StartFalling", fallDelay);
    }

    private void StartFalling()
    {
        rb.isKinematic = false; 
        rb.linearVelocity = new Vector2(0, -fallSpeed); 
        Destroy(gameObject, destroyDelay); 
    }
}