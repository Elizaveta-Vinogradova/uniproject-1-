using UnityEngine;

public class BouncyPlatforms : MonoBehaviour
{
    [SerializeField] 
    private float bounceForce;
    private Player player;
    void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        HandlePlayerBounce();
    }

    private void HandlePlayerBounce()
    {
        var rb = player.GetComponent<Rigidbody2D>();
        if (!rb) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
}