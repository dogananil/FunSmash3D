using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle1 : Obstacle
{

    private void Awake()
    {
        this.type = TYPE.TYPE1;
    }

    public override void Smash(Person gonnaDie)
    {
     PlayAnimation();
     Destroy(gonnaDie);
    }

    public override void PlayAnimation()
    {
        
    }
    
}
