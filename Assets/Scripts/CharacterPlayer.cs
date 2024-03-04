using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    
    public float speed = 5f;

    public float BulletCD = 3;
    private float timeFiredBullet = 1000;  // So that the first bullet can be fired immediately

    [SerializeField]
    private GameObject bulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Position
        var mousePosition = Input.mousePosition;
        // Convert Mouse Position to World Position
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 这里的手感感觉有点怪不知道是为什么
        // 莫名出现了出现了惯性的感觉
        var displacement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime;
        this.transform.position += displacement;

        // Fire Bullet
        if(BulletCD <= timeFiredBullet)
        {
            timeFiredBullet = 0;
            var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            var direction = (worldPosition - this.transform.position).normalized;
            bullet.GetComponent<Bullet>().__init__(this.transform.position, direction);
        }
        else
        {
            timeFiredBullet += Time.deltaTime;
        }
    }
}
