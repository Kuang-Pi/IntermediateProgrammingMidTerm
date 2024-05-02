using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public float speed = 4f;
    [HideInInspector]public float HP = 10f;
    [HideInInspector] public float damage = 2f;

    [SerializeField] private GameObject EXPPrefab;

    // Update is called once per frame
    void Update()
    {
        if(HP > 0)
        {
            Move(GameManager.instance.playerLocation);
        }
        else
        {
            this.OnDeath();
        }

    }
    protected void Move(Vector3 targetPosition)
    {
        this.transform.position += (targetPosition - this.transform.position).normalized * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            var otherObject = collision.gameObject.GetComponentInChildren<WeaponObject>();
            this.HP -= otherObject.damage;
            if(otherObject is Bullet)
            {
                Destroy(otherObject.gameObject);
            }
        }
    }

    protected virtual void OnDeath()
    {
        Instantiate(EXPPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
