using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : WeaponObject
{
    // Considering there might be effect that change the speed of the bullet after it is fired. 
    [HideInInspector]public float speed = 12f;
    private Vector3 directionBullet = new();
    [HideInInspector] public float range = 10f;
    private float distanceTraveled = 0f;
    
    private void Update()
    {
        this.transform.position += directionBullet * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled >= range)
        {
            Destroy(this.gameObject);
        }
    }

    public void __init__(Vector3 position, Vector3 direction, float speed = 6f)
    {
        this.transform.position = position;
        directionBullet = direction.normalized;
        directionBullet.z = 0;
        // Facing
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(directionBullet.y, directionBullet.x) * Mathf.Rad2Deg +90);
    }
}
