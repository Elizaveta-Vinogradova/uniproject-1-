using UnityEngine;
using UnityEngine.SceneManagement;


public class boundariesNotToFallOut : MonoBehaviour
{
    private Player player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
