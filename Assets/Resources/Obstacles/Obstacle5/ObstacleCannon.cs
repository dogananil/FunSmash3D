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
        MakeTransparent();
    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material, BlendMode.Transparent);
        ChangeRenderMode(cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material, BlendMode.Transparent);


        cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.r, cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.g, cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 0.3f);
        cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.r, cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.g, cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 0.3f);

    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material, BlendMode.Opaque);
        ChangeRenderMode(cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material, BlendMode.Opaque);


        cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.r, cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.g, cannonBall.transform.parent.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 1f);
        cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.r, cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.g, cannonBall.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 1f);

    }
    private void Start()
    {
        start = body.transform.localPosition + transform.position;
    }

    public override void Smash()
    {
        this.obstacleParticle.SetActive(false);
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
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
    

}
