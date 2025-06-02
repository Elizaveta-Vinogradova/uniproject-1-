using UnityEngine;

public class TeleportationLocation : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;

    void OnTriggerEnter2D(Collider2D other)
    {
        if  (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3(x, y, -1);
            Destroy(gameObject);
        }
    }
}
