using UnityEngine;
using System.Collections;

public class NewSecretArea : MonoBehaviour
{
    [Header("Disappearance settings")]
    [SerializeField] private float fadeDuration = 2f;   
    [SerializeField] private float visibleAlpha = 1f;
    
    private SpriteRenderer spriteRenderer;
    private Coroutine currentFadeCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        var initialColor = spriteRenderer.color;
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            currentFadeCoroutine = StartCoroutine(FadeSprite(visibleAlpha));
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            currentFadeCoroutine = StartCoroutine(FadeSprite(0f));
    }

    private IEnumerator FadeSprite(float targetAlpha)
    {
        var startColor = spriteRenderer.color;
        var targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        
        var elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            var newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = targetColor;
    }
}