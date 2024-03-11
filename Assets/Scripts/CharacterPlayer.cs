using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlayer : MonoBehaviour
{

    public static CharacterPlayer instance;

    public float speed = 5f;

    public float BulletCD = 3;
    private float timeFiredBullet = 1000;  // So that the first bullet can be fired immediately
    
    [HideInInspector]public float EXPFactor = 1f;
    [HideInInspector] public float EXP = 0f;
    [HideInInspector] public int playerLevel = 0;
    private float previousEXP = 1f;
    private float previousPreviousEXP = 1f;

    [SerializeField]private GameObject bulletPrefab;

    [SerializeField] private Slider HPBar;

    [HideInInspector]public float HP = 10f;
    [HideInInspector] public float MaxHP = 10f;

    private void Awake()
    {
        if(CharacterPlayer.instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // HP Bar
        HPBar.value = HP/MaxHP;

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

        // Experience and Level
        if (EXP >= previousEXP+previousPreviousEXP)
        {
            playerLevel += 1;
            // Save any over needed EXP to next level
            EXP -= previousEXP + previousPreviousEXP;
            // Update the EXP need for next level
            // Fibonacci sequence
            previousEXP += previousPreviousEXP;
            previousPreviousEXP = previousEXP - previousPreviousEXP;

            // 升级事件，以后再写
            print("LevelUp! You're level " + playerLevel);
        }
    }

    public void GainEXP(float amount)
    {
        this.EXP += amount * EXPFactor;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<Enemy>())
        {
            this.HP -= collision.gameObject.GetComponentInChildren<Enemy>().damage;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponentInChildren<Collectable>())
        {

        }
    }
}
