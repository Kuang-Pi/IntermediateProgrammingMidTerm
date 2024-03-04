using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Considering there might be effect that change the speed of the bullet after it is fired. 
    public float speed = 6f;
    
    public void __init__(Vector3 position, Vector3 direction, float speed = 6f)
    {
        this.transform.position = position;
        this.GetComponent<Rigidbody2D>().velocity = direction * speed;
        // Facing
        this.transform.rotation = Quaternion.LookRotation(direction);
    }
}
