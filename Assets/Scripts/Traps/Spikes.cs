using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float bounceForce = 10f;
    private Player player;
    private int damage;
    void Start()
    {
        damage = 1;
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        player.GetComponent<Health>().TakeDamage(damage);
    }
}
