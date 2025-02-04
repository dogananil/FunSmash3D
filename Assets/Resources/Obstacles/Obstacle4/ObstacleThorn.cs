﻿using System;
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
        MakeTransparent();

    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material, BlendMode.Transparent);
        ChangeRenderMode(thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material, BlendMode.Transparent);


        ChangeRenderMode(thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material, BlendMode.Transparent);
        ChangeRenderMode(thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material, BlendMode.Transparent);



        thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.r, thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.g, thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 0.3f);
        thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.r, thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.g, thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 0.3f);

        thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.r, thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.g, thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 0.3f);
        thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.r, thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.g, thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 0.3f);

    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(thornRight.transform.GetChild(0).GetComponentInChildren<MeshRenderer>().material, BlendMode.Opaque);
        ChangeRenderMode(thornRight.transform.GetChild(1).GetComponentInChildren<MeshRenderer>().material, BlendMode.Opaque);


        ChangeRenderMode(thornLeft.transform.GetChild(0).GetComponentInChildren<MeshRenderer>().material, BlendMode.Opaque);
        ChangeRenderMode(thornLeft.transform.GetChild(1).GetComponentInChildren<MeshRenderer>().material, BlendMode.Opaque);



        thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.r, thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.g, thornRight.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 1f);
        thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.r, thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.g, thornRight.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 1f);

        thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.r, thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.g, thornLeft.transform.GetChild(0).GetComponent<MeshRenderer>().material.color.b, 1f);
        thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = new Color(thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.r, thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.g, thornLeft.transform.GetChild(1).GetComponent<MeshRenderer>().material.color.b, 1f);

    }
    private void Start()
    {
        startRight = bodyRight.transform.localPosition + transform.position;

        startLeft = bodyLeft.transform.localPosition + transform.position;
    }

    public override void Smash()
    {
        obstacleParticle.SetActive(false);
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
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
    

}
