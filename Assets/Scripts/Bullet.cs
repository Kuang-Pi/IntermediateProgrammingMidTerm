using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Considering there might be effect that change the speed of the bullet after it is fired. 
    public float speed = 6f;
    private Vector3 directionBullet = new();
    
    private void Update()
    {
        this.transform.position += directionBullet * speed * Time.deltaTime;
    }

    public void __init__(Vector3 position, Vector3 direction, float speed = 6f)
    {
        this.transform.position = position;
        directionBullet = direction.normalized;
        // Facing
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(directionBullet.y, directionBullet.x) * Mathf.Rad2Deg);
    }
}
