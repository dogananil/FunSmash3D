using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleThorn : Obstacle
{
    [SerializeField] private GameObject thornRight;
    [SerializeField] private GameObject thornLeft;
    [SerializeField] private AnimationCurve speedCurve2;
    [System.NonSerialized] private Rigidbody bodyRight;
    [System.NonSerialized] private Rigidbody bodyLeft;
    [System.NonSerialized] private Vector3 startRight;
    [System.NonSerialized] private Vector3 startLeft;

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE4;
        bodyRight = thornRight.GetComponent<Rigidbody>();
        bodyLeft = thornLeft.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startRight = bodyRight.transform.localPosition + transform.position;

        startLeft = bodyLeft.transform.localPosition + transform.position;
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
            bodyRight.MovePosition(startRight + bodyRight.transform.up * speedCurve2.Evaluate(timeStep)*4f);
            bodyLeft.MovePosition(startLeft + bodyLeft.transform.up * speedCurve2.Evaluate(timeStep)*4f);
            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }

        TabController.INSTANCE.run = true;
    }

}
