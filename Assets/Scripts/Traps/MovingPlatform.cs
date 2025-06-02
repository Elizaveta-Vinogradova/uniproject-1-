using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float PlatformSpeed = 1f;

    private Vector3 nextPosition;

    private void Start() => nextPosition = PointB.position;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, PlatformSpeed * Time.deltaTime);
        if (transform.position == nextPosition)
            nextPosition = (nextPosition == PointA.position) ? PointB.position : PointA.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        collision.transform.SetParent(transform, true);
        collision.gameObject.transform.localScale = new Vector3(1, 1, -1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.transform.parent = null;
    }
}