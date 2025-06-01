using UnityEngine;

public class NPC_talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Animator dad;
    public Animator interactAnim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        dad = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        anim.Play("Idle");
        interactAnim.Play("Open");
    }

    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic = false;
    }
}
