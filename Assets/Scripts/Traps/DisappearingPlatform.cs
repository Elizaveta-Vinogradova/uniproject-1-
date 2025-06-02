using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [Header("Settings Platform")] 
    public bool IsColliderDisabled;
    public float Smooth = 0.1f;
    public float TimeStartCycle;
    
    [Header("Settings Disappear")]
    public float TimeDisappearance;
    public float DisappearWaitingTime;
    public float BorderForDissapear = 0.2f;
    
    [Header("Settings Appear")]
    public float TimeAppearance;
    public float AppearWaitingTime;
    public float BorderForAppear = 0.2f;
    
    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(nameof(StartOffset));
    }

    private IEnumerator StartOffset()
    {
        yield return new WaitForSeconds(TimeStartCycle);
        yield return StartCoroutine(nameof(WaitingBeforeDisappear));
    }

    private IEnumerator StartDisappear()
    {
        var color = spriteRenderer.color;
        for (var elapsedTime = 0.0f; elapsedTime < TimeDisappearance; elapsedTime += Smooth)
        {
            color.a = 1 - elapsedTime / TimeDisappearance;
            spriteRenderer.color = color;
            if (IsColliderDisabled && spriteRenderer.color.a < BorderForDissapear && boxCollider.enabled)
                boxCollider.enabled = false;
            yield return new WaitForSeconds(Smooth);
        }

        color.a = 0.0f;
        spriteRenderer.color = color;
        yield return StartCoroutine(nameof(WaitingBeforeAppear));
    }

    private IEnumerator StartAppear()
    {
        var color = spriteRenderer.color;
        for (var elapsedTime = 0.0f; elapsedTime < TimeAppearance; elapsedTime += Smooth)
        {
            color.a = elapsedTime / TimeAppearance;
            spriteRenderer.color = color;
            if (IsColliderDisabled && spriteRenderer.color.a > BorderForAppear && !boxCollider.enabled)
                boxCollider.enabled = true;
            yield return new WaitForSeconds(Smooth);
        }

        color.a = 1.0f;
        spriteRenderer.color = color;
        yield return StartCoroutine(nameof(WaitingBeforeDisappear));
    }

    private IEnumerator WaitingBeforeDisappear()
    {
        for (var elapsedTime = 0.0f; elapsedTime < DisappearWaitingTime; elapsedTime += Smooth)
            yield return new WaitForSeconds(Smooth);
        yield return StartCoroutine(nameof(StartDisappear));
    }
    
    private IEnumerator WaitingBeforeAppear()
    {
        for (var elapsedTime = 0.0f; elapsedTime < AppearWaitingTime; elapsedTime += Smooth)
            yield return new WaitForSeconds(Smooth);
        yield return StartCoroutine(nameof(StartAppear));
    }
}
