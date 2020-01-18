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
