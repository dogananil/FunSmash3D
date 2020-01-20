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
        MakeTransparent();
    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(hammer.GetComponent<MeshRenderer>().material, BlendMode.Transparent);


        hammer.GetComponent<MeshRenderer>().material.color = new Color(hammer.GetComponent<MeshRenderer>().material.color.r, hammer.GetComponent<MeshRenderer>().material.color.g, hammer.GetComponent<MeshRenderer>().material.color.b, 0.3f);
    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(hammer.GetComponent<MeshRenderer>().material, BlendMode.Opaque);


        hammer.GetComponent<MeshRenderer>().material.color = new Color(hammer.GetComponent<MeshRenderer>().material.color.r, hammer.GetComponent<MeshRenderer>().material.color.g, hammer.GetComponent<MeshRenderer>().material.color.b, 1f);

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
            
            body.MoveRotation(Quaternion.Euler(speedCurve2.Evaluate(timeStep) * -60.0f, 0.0f, 0.0f));

            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
   

}
