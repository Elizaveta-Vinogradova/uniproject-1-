using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = System.Numerics.Vector3;

public class parallax : MonoBehaviour
{
    public float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        var temp = cam.transform.position.x * (1 - parallaxEffect);
        var dist = cam.transform.position.x * parallaxEffect;
        
        transform.position = new UnityEngine.Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) 
            startpos += length;
        else if (temp < startpos - length)
            startpos -= length;
    }
}
