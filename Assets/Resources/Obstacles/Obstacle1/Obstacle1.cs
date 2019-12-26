using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle1 : Obstacle
{
    [SerializeField] private GameObject boxHand;
    [SerializeField] private AnimationCurve speedCurve;
    private void Awake()
    {
        this.type = TYPE.TYPE1;
    }

    public override void Smash()
    {
     StartCoroutine(PlayAnimation());
    // Destroy(gonnaDie);
    }

    public override IEnumerator PlayAnimation()
    {
        Vector3 startPosition = boxHand.transform.position;
        float timeStep=0f;
        while (timeStep<1f)
        {
            boxHand.transform.position=startPosition-boxHand.transform.up * speedCurve.Evaluate(timeStep)*boxHand.transform.localScale.z*2 ;
            timeStep += Time.deltaTime;
            yield return null;
        }
        boxHand.transform.position=startPosition-boxHand.transform.up * speedCurve.Evaluate(1.0f)*boxHand.transform.localScale.z*2 ;
    }
    
}
