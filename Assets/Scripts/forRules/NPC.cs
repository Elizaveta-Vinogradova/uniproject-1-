using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour 
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public static bool playerIsClose;
    [SerializeField] RectTransform dialoguePanelRect;
    private Animator anim;
    [SerializeField] private AudioClip speechSound;

    void Start() => anim = GetComponent<Animator>();
    void FixedUpdate()
    {
        if (playerIsClose)
        {
            anim.Play("wt_idle");
            if (!dialoguePanel.activeInHierarchy)
            {
                SoundManager.instance.PlaySound(speechSound);
                dialoguePanelRect.transform.position = new Vector3(transform.position.x, 0.45f ,0);
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index] && index != dialogue.Length - 1)
            contButton.SetActive(true);
    }

    private void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        anim.Play("wt_run");
    }

    IEnumerator Typing()
    {
        foreach (var letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        SoundManager.instance.PlaySound(speechSound);
        contButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
            ZeroText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(other.transform.localScale.x > 0 && transform.localScale.x < 0 || other.transform.localScale.x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        playerIsClose = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerIsClose = false;
        ZeroText();
    }
}
