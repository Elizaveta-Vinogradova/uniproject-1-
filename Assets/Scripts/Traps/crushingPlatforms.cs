using UnityEngine;
public class CrushingPlatforms : MonoBehaviour
{ 
    [Header ("Movement")]
    public Transform PointA;
    public Transform PointB;
    public float PlatformSpeed = 1f;

    private Vector3 nextPosition;
    [Header("Sounds")]
    [SerializeField] AudioClip moveSound;
    private TrapSound trapSound;

    private void Start()
    {
        nextPosition = PointB.position;
        trapSound = GetComponent<TrapSound>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, PlatformSpeed * Time.deltaTime);
        if (transform.position != nextPosition) return;
        if (trapSound != null)
            trapSound.PlayMovementSound();
        nextPosition = (nextPosition == PointA.position) ? PointB.position : PointA.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Health>().TakeDamage(10);
    }
}
