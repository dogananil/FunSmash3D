using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle1 : Obstacle
{
    [SerializeField] private GameObject boxHand;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private AnimationCurve speedCurve2;
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
        Rigidbody body = boxHand.GetComponent<Rigidbody>();
        /*Rigidbody body = boxHand.GetComponent<Rigidbody>();

        body.velocity = -boxHand.transform.up;
        
        yield return new WaitForSeconds(1.0f); 
        
        body.velocity = boxHand.transform.up;
        
        yield return new WaitForSeconds(1.0f);*/
        Vector3 startPosition = boxHand.transform.position;
        float timeStep=0f;
        /*while (timeStep<1f)
        {
            boxHand.transform.position=startPosition-boxHand.transform.up * speedCurve.Evaluate(timeStep)*boxHand.transform.localScale.z*2 ;
            timeStep += Time.deltaTime;
            yield return null;
        }
        boxHand.transform.position=startPosition-boxHand.transform.up * speedCurve.Evaluate(1.0f)*boxHand.transform.localScale.z*2 ;*/
        while (timeStep<1f)
        {
            body.velocity=-boxHand.transform.up*boxHand.transform.localScale.z*2*speedCurve2.Evaluate(timeStep)*2f;
            timeStep += Time.deltaTime;
            yield return null;
        }
       body.velocity=Vector3.zero;
    }
    
}
