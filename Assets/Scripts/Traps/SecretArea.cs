using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;

    private SpriteRenderer spriteRenderer;
    private Color hiddenColor;
    private Coroutine currentColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hiddenColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            currentColor = StartCoroutine(FadeSprite(true));
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            currentColor = StartCoroutine(FadeSprite(false));
    }

    private IEnumerator FadeSprite(bool fadeOut)
    {
        var startColor = spriteRenderer.color;
        var targetColor = fadeOut ? new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0f) : hiddenColor;
        var timeFading = 0f;
        while (timeFading < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, timeFading / fadeDuration);
            timeFading += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }
}
