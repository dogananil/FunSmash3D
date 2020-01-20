using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstacleCannonBall : Obstacle
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private AnimationCurve speedCurve2;
    [System.NonSerialized] private Rigidbody body;
  

    private void Awake()
    {
        this.obstacleType = TYPE.TYPE3;
        body = cannonBall.GetComponent<Rigidbody>();
        MakeTransparent();
    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(cannonBall.GetComponent<MeshRenderer>().material, BlendMode.Transparent);


        cannonBall.GetComponent<MeshRenderer>().material.color = new Color(cannonBall.GetComponent<MeshRenderer>().material.color.r, cannonBall.GetComponent<MeshRenderer>().material.color.g, cannonBall.GetComponent<MeshRenderer>().material.color.b, 0.3f);

    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(cannonBall.GetComponent<MeshRenderer>().material, BlendMode.Opaque);


        cannonBall.GetComponent<MeshRenderer>().material.color = new Color(cannonBall.GetComponent<MeshRenderer>().material.color.r, cannonBall.GetComponent<MeshRenderer>().material.color.g, cannonBall.GetComponent<MeshRenderer>().material.color.b, 1f);

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
            body.MoveRotation(Quaternion.Euler(speedCurve2.Evaluate(timeStep) * 90, 0.0f, 0.0f));

            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
   

}
