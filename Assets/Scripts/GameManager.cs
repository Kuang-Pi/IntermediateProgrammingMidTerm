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
    }
}
