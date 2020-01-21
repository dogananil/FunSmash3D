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
        this.obstacleType = TYPE.TYPE1;
        body = boxHand.GetComponent<Rigidbody>();
        MakeTransparent();
    }

    private void Start()
    {
        base.Start();
        start = body.transform.localPosition + transform.position;

    }

    public override void Smash()
    {
        this.obstacleParticle.SetActive(false);
        StartCoroutine(PlayAnimation());
    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(boxHand.GetComponent<MeshRenderer>().material, BlendMode.Transparent);

        boxHand.GetComponent<MeshRenderer>().material.color = new Color(boxHand.GetComponent<MeshRenderer>().material.color.r, boxHand.GetComponent<MeshRenderer>().material.color.g, boxHand.GetComponent<MeshRenderer>().material.color.b, 0.3f);
        
    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(boxHand.GetComponent<MeshRenderer>().material, BlendMode.Opaque);
        boxHand.GetComponent<MeshRenderer>().material.color = new Color(boxHand.GetComponent<MeshRenderer>().material.color.r, boxHand.GetComponent<MeshRenderer>().material.color.g, boxHand.GetComponent<MeshRenderer>().material.color.b, 1f);

    }
    public override IEnumerator PlayAnimation()
    {
        float timeStep=0f;
        while (timeStep<1f)
        {
            body.MovePosition(start + body.transform.forward * speedCurve2.Evaluate(timeStep) * 10.0f);    
            timeStep += Time.deltaTime * obstacleSpeed;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
    

}
