using System;
using UnityEngine;

public class PlatformTorch : MonoBehaviour
{
    private Player player;
    void Start()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<Player>();
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}