using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBetweenScenes : MonoBehaviour
{
    [SerializeField] public int neededAmount;
    private Player player; 
    
    void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || player == null || player.AmountOfGrains < neededAmount) return;
        var sceneToLoadNumber = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneToLoadNumber == 5)
            sceneToLoadNumber = 0;
        PlayerPrefs.SetInt("savedScene",sceneToLoadNumber);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneToLoadNumber);
    }
}