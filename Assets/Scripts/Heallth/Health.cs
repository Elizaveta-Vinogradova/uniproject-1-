using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public int startingHealth;
    public int currentHealth { get;  set; }
    private bool dead;
    private Animator anim;
    private Player player;

    private void Awake()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }
    public async void TakeDamage(int damage)
    {
        if (dead) return;
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            ScoreManager.instance.LoseHP(damage);
            anim.SetTrigger("hurt");
            return;
        }
        dead = true;
        player.AmountOfGrains = 0;
        anim.SetTrigger("dead");
        await Task.Delay(1000);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}