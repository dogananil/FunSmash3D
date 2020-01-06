using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleBox : Obstacle
{
    [SerializeField] private GameObject boxHand;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private AnimationCurve speedCurve2;
    [System.NonSerialized] private Rigidbody body;
    [System.NonSerialized] private Vector3 start;

    private void Awake()
    {
        this.type = TYPE.TYPE1;
        body = boxHand.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        start = body.transform.localPosition + transform.position;
    }

    public override void Smash()
    {
        StartCoroutine(PlayAnimation());
    // Destroy(gonnaDie);
    }

    public override IEnumerator PlayAnimation()
    {
        float timeStep=0f;
        while (timeStep<1f)
        {
            body.MovePosition(start + body.transform.forward * speedCurve2.Evaluate(timeStep) * 10.0f);    
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }
    
}
