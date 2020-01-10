using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleChillBase : Obstacle
{
   
   

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE0;
       
    }

  

    public override void Smash()
    {
       // StartCoroutine(PlayAnimation());
        // Destroy(gonnaDie);
    }

    public override IEnumerator PlayAnimation()
    {
        float timeStep = 0f;
        while (timeStep < 1f)
        {
           
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }

}
