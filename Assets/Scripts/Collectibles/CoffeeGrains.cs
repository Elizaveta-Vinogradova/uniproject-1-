using UnityEngine;

public class CoffeeGrains : MonoBehaviour
{
    private Player player; 
    [SerializeField] public int scoreValue;

    [SerializeField] private AudioClip collectSound;
    void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        player.AmountOfGrains += scoreValue;
        SoundManager.instance.PlaySound(collectSound);
        ScoreManager.instance.AddCoffeePoint(scoreValue);
        Destroy(gameObject);
    }
}
