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
    [System.NonSerialized] private Quaternion start;
    [System.NonSerialized] private Quaternion end = new Quaternion(-90,0,0,0);

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE1;
       
    }

    private void Start()
    {
        start =hammer.transform.localRotation;
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
            // body.MovePosition(start + body.transform.forward * speedCurve2.Evaluate(timeStep) * 10.0f);
            //hammer.transform.rotation = Quaternion.Slerp(start, end , speedCurve2.Evaluate(timeStep));
            hammer.transform.rotation = Quaternion.RotateTowards(start, end , speedCurve2.Evaluate(timeStep));
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }

}
