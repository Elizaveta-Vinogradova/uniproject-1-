using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;

    [SerializeField] private Transform player;
    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}