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
