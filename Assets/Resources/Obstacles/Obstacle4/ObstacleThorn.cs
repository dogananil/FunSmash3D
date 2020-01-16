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
        MakeTransparent();

    }
    public override void MakeTransparent()
    {
        ChangeRenderMode(bodyRight.GetComponent<MeshRenderer>().material, BlendMode.Fade);


        ChangeRenderMode(bodyLeft.GetComponent<MeshRenderer>().material, BlendMode.Fade);



        bodyRight.GetComponent<MeshRenderer>().material.color = new Color(bodyRight.GetComponent<MeshRenderer>().material.color.r, bodyRight.GetComponent<MeshRenderer>().material.color.g, bodyRight.GetComponent<MeshRenderer>().material.color.b, 0.3f);
        bodyLeft.GetComponent<MeshRenderer>().material.color = new Color(bodyLeft.GetComponent<MeshRenderer>().material.color.r, bodyLeft.GetComponent<MeshRenderer>().material.color.g, bodyLeft.GetComponent<MeshRenderer>().material.color.b, 0.3f);
    }
    public override void MakeOpaque()
    {
        ChangeRenderMode(bodyRight.GetComponent<MeshRenderer>().material, BlendMode.Opaque);


        ChangeRenderMode(bodyLeft.GetComponent<MeshRenderer>().material, BlendMode.Opaque);



        bodyRight.GetComponent<MeshRenderer>().material.color = new Color(bodyRight.GetComponent<MeshRenderer>().material.color.r, bodyRight.GetComponent<MeshRenderer>().material.color.g, bodyRight.GetComponent<MeshRenderer>().material.color.b, 1f);
        bodyLeft.GetComponent<MeshRenderer>().material.color = new Color(bodyLeft.GetComponent<MeshRenderer>().material.color.r, bodyLeft.GetComponent<MeshRenderer>().material.color.g, bodyLeft.GetComponent<MeshRenderer>().material.color.b, 1f);
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
        StartCoroutine(TextAnimation());
        TabController.INSTANCE.run = true;
    }
    private IEnumerator TextAnimation()
    {
        Vector3 startPosition = LevelManager.instance.currentEnemy.transform.position;
        LevelManager.instance.currentEnemy.deatCount.transform.gameObject.SetActive(true);
        LevelManager.instance.currentEnemy.deatCount.transform.forward = Camera.main.transform.forward;
        if (Obstacle.deathCounter <= 5)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "x" + Obstacle.deathCounter.ToString();
        }
        else if (Obstacle.deathCounter > 5 && Obstacle.deathCounter <= 15)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "NICE" + "\n" + "x" + Obstacle.deathCounter.ToString();

        }
        else if (Obstacle.deathCounter > 15 && Obstacle.deathCounter <= 40)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "AWESOME" + "\n" + "x" + Obstacle.deathCounter.ToString();

        }
        Obstacle.deathCounter = 0;
        float timeStep = 0f;

        while (timeStep < 5)
        {

            LevelManager.instance.currentEnemy.deatCount.transform.position = startPosition + new Vector3(0, textCurve.Evaluate(timeStep), 2.0f);
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        LevelManager.instance.currentEnemy.deatCount.transform.gameObject.SetActive(false);




    }

}
