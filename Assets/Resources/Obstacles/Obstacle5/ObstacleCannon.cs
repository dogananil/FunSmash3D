using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleCannon : Obstacle
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private AnimationCurve speedCurve2;
    [System.NonSerialized] private Rigidbody body;
    [System.NonSerialized] private Vector3 start;

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE5;
        body = cannonBall.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        start = body.transform.localPosition + transform.position;
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
            //body.MovePosition(start + body.transform.right * speedCurve2.Evaluate(timeStep) * 10.0f);
            body.AddForce(body.transform.right * speedCurve2.Evaluate(timeStep) * 10.0f, ForceMode.VelocityChange);
            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }

}
