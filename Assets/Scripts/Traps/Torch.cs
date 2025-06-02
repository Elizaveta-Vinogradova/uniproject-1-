using System;
using UnityEngine;

public class LanternPuzzle : MonoBehaviour
{
    public int orderID; 
    public GameObject hiddenPlatform; 
    public float transitionSpeed = 2.0f;
    private Player player;
    private SpriteRenderer spriteRenderer;
    private Color targetColor;
    private static LanternPuzzle[] allLanterns;
    private static int currentOrder = 1;

    private void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        allLanterns = FindObjectsOfType<LanternPuzzle>();
        targetColor = Color.gray; 
        spriteRenderer.color = targetColor;
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 3f)
                ToggleLantern();
        }

        if (spriteRenderer.color != targetColor)
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * transitionSpeed);
    }

    private void ToggleLantern()
    {
        if (spriteRenderer.color != Color.gray && spriteRenderer.color != Color.white) return;
        var isCurrentlyLit = (spriteRenderer.color == Color.white);
        if (!isCurrentlyLit)
        {
            if (orderID == currentOrder)
            {
                targetColor = Color.white;
                currentOrder++;
                CheckPuzzleComplete();
            }
            else
                ResetAllLanterns();
        }
        else
            targetColor = Color.gray;
    }

    private void CheckPuzzleComplete()
    {
        if (currentOrder > allLanterns.Length)
            hiddenPlatform.SetActive(true);
    }

    private void ResetAllLanterns()
    {
        currentOrder = 1;
        foreach (var lantern in allLanterns)
        {
            lantern.targetColor = Color.gray;
        }
    }
}