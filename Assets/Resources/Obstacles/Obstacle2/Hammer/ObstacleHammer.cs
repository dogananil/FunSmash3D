using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleHammer : Obstacle
{
    [SerializeField] private GameObject hammer;
    [SerializeField] private AnimationCurve speedCurve2;
    [System.NonSerialized] private Rigidbody body;


    private void Awake()
    {
        this.obstacleType = TYPE.TYPE2;
        body = hammer.GetComponent<Rigidbody>();
    }

    public override void Smash()
    {
        StartCoroutine(PlayAnimation());
        
    }

    public override IEnumerator PlayAnimation()
    {
        float timeStep = 0f;
        while (timeStep < 1f)
        {
            
            body.MoveRotation(Quaternion.Euler(speedCurve2.Evaluate(timeStep) * -60.0f, 0.0f, 0.0f));

            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }

}
