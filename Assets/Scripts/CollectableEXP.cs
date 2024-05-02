using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableEXP : Collectable
{
    [HideInInspector]public float EXPGiving = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPlayerCollect()
    {
        CharacterPlayer.instance.GainEXP(EXPGiving);
        base.OnPlayerCollect();
    }
}
