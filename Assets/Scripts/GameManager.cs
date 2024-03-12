using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;
    [HideInInspector]
    public Vector3 playerLocation = new();

    [SerializeField]
    private GameObject playerObject;

    private float timeSinceLastSpawn = 0f;
    private float spawnCD = 5f;
    [SerializeField] private GameObject enemyPrefab;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get player's current location. 
        playerLocation = playerObject.transform.position;

        // Spawn some enemy
        if (Time.time - timeSinceLastSpawn > spawnCD)
        {
            timeSinceLastSpawn = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Spawn an enemy at a random location
        var enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
    }
}
