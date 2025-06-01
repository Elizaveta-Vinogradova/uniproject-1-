using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthForOther : MonoBehaviour
{
    [Header("If influences")] [SerializeField]
    private bool ifArrowInfluence;
    private bool ikKnifeInfluence;
    
    [Header ("Health")]
    [SerializeField] private int startingHealth;
    
    private int currentHealth { get;  set; }
    private bool dead;
    
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }
    public void TakeDamage(int damage)
    {
        if (dead) return;
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            Debug.Log($"{currentHealth}");
            anim.SetTrigger("hurt");
            return;
        }
        SoundManager.instance.PlaySound(deathSound);
        Debug.Log($"{currentHealth}");
        dead = true;
        anim.SetTrigger("dead");
        foreach (var component in components)
            Destroy(component);
        Destroy(gameObject);
    }
}