using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterPlayer : MonoBehaviour
{

    public static CharacterPlayer instance;

    public float speed = 5f;

    public enum WeaponSlots
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private GameObject BasicAttack;

    public GameObject upSlot;
    public GameObject downSlot;
    public GameObject leftSlot;
    public GameObject rightSlot;
    
    [HideInInspector] public float EXPFactor = 1f;
    [HideInInspector] public float EXP = 0f;
    [HideInInspector] public int playerLevel = 0;
    private float previousEXP = 1f;
    private float previousPreviousEXP = 1f;

    [SerializeField] private Slider HPBar;
    [SerializeField] private TextMeshProUGUI LevelText;

    [HideInInspector] public float HP = 10f;
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
        // Death
        if(HP <= 0)
        {
            print("GameOver");
            // Do a Game Over Screen future
            SceneManager.LoadScene("Menu");
        }
        
        // HP Bar
        HPBar.value = HP/MaxHP;

        // Level
        LevelText.text = "Level " + playerLevel + ": " +  EXP + " / " + (previousEXP + previousPreviousEXP).ToString();

        // Get Mouse Position
        var mousePosition = Input.mousePosition;
        // Convert Mouse Position to World Position
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 这里的手感感觉有点怪不知道是为什么
        // 莫名出现了出现了惯性的感觉
        var displacement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * speed * Time.deltaTime;
        this.transform.position += displacement;

        // Check for rest ammo. 

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

            #region LevelUp
            // When Level up , the player's HP will be fully restored
            HP = MaxHP;
            // Freeze the game
            Time.timeScale = 0;
            // The unfreeze happens when the player made their choice. 
            // When Level up, player will have 2 choices
            // Upgrade a existing weapon, or learn a new weapon
            
            #endregion

        }
        // Cheat
        if (Input.GetKeyDown(KeyCode.F))
        {
            upSlot = Instantiate(BasicAttack, this.transform);
            downSlot = Instantiate(BasicAttack, this.transform);
            leftSlot = Instantiate(BasicAttack, this.transform);
            rightSlot = Instantiate(BasicAttack, this.transform);

            upSlot.transform.SetLocalPositionAndRotation(new Vector3(0, 1, 0), Quaternion.identity);
            downSlot.transform.SetLocalPositionAndRotation(new Vector3(0, -1, 0), Quaternion.identity);
            leftSlot.transform.SetLocalPositionAndRotation(new Vector3(-1, 0, 0), Quaternion.identity);
            rightSlot.transform.SetLocalPositionAndRotation(new Vector3(1, 0, 0), Quaternion.identity);

            OnChangingWeapon(WeaponSlots.Up, true);
            OnChangingWeapon(WeaponSlots.Down, true);
            OnChangingWeapon(WeaponSlots.Left, true);
            OnChangingWeapon(WeaponSlots.Right, true);

        }
    }

    public void OnChangingWeapon(WeaponSlots slot, bool isTakingNewWeapon)
    {
        if (isTakingNewWeapon)
        {
            switch (slot)
            {
                case WeaponSlots.Up:
                    upSlot.GetComponentInChildren<Weapon>().direction = new Vector3(0, 1, 0);
                    upSlot.GetComponentInChildren<Weapon>().weaponSlot = slot;
                    break;
                case WeaponSlots.Down:
                    downSlot.GetComponentInChildren<Weapon>().direction = new Vector3(0, -1, 0);
                    downSlot.GetComponentInChildren<Weapon>().weaponSlot = slot;
                    break;
                case WeaponSlots.Left:
                    leftSlot.GetComponentInChildren<Weapon>().direction = new Vector3(-1, 0, 0);
                    leftSlot.GetComponentInChildren<Weapon>().weaponSlot = slot;
                    break;
                case WeaponSlots.Right:
                    rightSlot.GetComponentInChildren<Weapon>().direction = new Vector3(1, 0, 0);
                    rightSlot.GetComponentInChildren<Weapon>().weaponSlot = slot;
                    break;
            }
        }
        else
        {
            switch (slot)
            {
                case WeaponSlots.Up:
                    upSlot = null;
                    break;
                case WeaponSlots.Down:
                    downSlot = null;
                    break;
                case WeaponSlots.Left:
                    leftSlot = null;
                    break;
                case WeaponSlots.Right:
                    rightSlot = null;
                    break;
            }
        }
        // Play a sound here
    }

    public void GainEXP(float amount)
    {
        this.EXP += amount * EXPFactor;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Change all the collsions to their own scripts
        if (collision.gameObject.GetComponentInChildren<Enemy>())
        {
            this.HP -= collision.gameObject.GetComponentInChildren<Enemy>().damage;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponentInChildren<Collectable>())
        {
            // Discarded. CollectableEXP is used instead. 
        }
    }

}
