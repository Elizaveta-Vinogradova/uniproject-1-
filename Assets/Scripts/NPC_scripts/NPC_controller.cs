using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class NPC_controller : MonoBehaviour
{
    public enum NPCState { Default, Idle, Patrol, Talk}
    public static NPCState currentState = NPCState.Patrol;
    private NPCState defaultState;
    public NPC_patrol patrol;
    public NPC_talk talk;
    private Animator animator;
    void Start()
    {
        defaultState = currentState;
        SwitchState(currentState);
    }

    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        
        patrol.enabled = newState == NPCState.Patrol;
        talk.enabled = newState == NPCState.Talk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           SwitchState(NPCState.Talk);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
