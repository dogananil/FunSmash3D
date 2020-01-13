using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleFinishBase : Obstacle
{

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE6;
    }


    public override void Smash()
    {
        
    }

    public override IEnumerator PlayAnimation()
    {
        float timeStep = 0f;
        while (timeStep < 1f)
        {
          
            yield return new WaitForEndOfFrame(); }
    }

}
