
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Player player;
    private int damage;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        anim.SetTrigger("explode");
        player.GetComponent<Health>().TakeDamage(damage);
    }
}