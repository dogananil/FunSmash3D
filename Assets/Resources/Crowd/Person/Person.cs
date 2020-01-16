using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Person : MonoBehaviour
{
    [SerializeField] public Animator run, finish;
    public GameObject personPrefab;
    [SerializeField] private AnimationCurve dieCurve;
    public float speed;
    private static bool stopRun = false;

    [SerializeField] private Color color;
    [System.NonSerialized] private MeshRenderer meshRenderer;
    [System.NonSerialized] public bool dead = false;
    [System.NonSerialized] public bool dead2 = false;
    private bool stop = false;
    private Collider otherTemp;
   


    private void Update()
    {
        if (this == null || stopRun || !TabController.INSTANCE.run || stop)
        {
            return;
        }
        this.personPrefab.transform.position += Vector3.right * Time.deltaTime * speed;
        //Debug.Log(speed);
        run.SetBool("run", TabController.INSTANCE.run);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            dead = true;
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            this.personPrefab.transform.SetParent(PersonPool.INSTANCE.transform);
            ParticleManager.instance.PlaySystem(ParticleManager.SYSTEM.HIT_SYSTEM, transform.position, color, 20);
            ParticleManager.instance.PlaySystem(ParticleManager.SYSTEM.DEATH_TRAIL, transform.position, Color.black, 1);
            /*if(!dead2)
            {
                PersonPool.INSTANCE.pool.Push(this);
                dead2 = true;
            }*/
            other.transform.GetComponentInParent<Obstacle>().deathCounter++;
            otherTemp = other;
            ScrollBar.INSTANCE.LoadProgessBar();
            StopPerson();
            StartCoroutine(DieSlowly(5.0f));

        }
        else if (other.transform.CompareTag("FinishBase"))
        {
            /*if (!dead2)
            {
                PersonPool.INSTANCE.pool.Push(this);
                dead2 = true;
            }*/
            StartCoroutine(FinishGame(this.speed / 2f));
        }
        /*else if (other.transform.CompareTag("DeathBase"))
        {
            *//*if (!dead2)
            {
                PersonPool.INSTANCE.pool.Push(this);
                dead2 = true;
            }*//*
            //Die();
        }*/

    }

    private IEnumerator DieSlowly(float seconds)
    {
        LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.gameObject.SetActive(true);

        float timeStep = 0f;
        LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.forward = Camera.main.transform.forward;
       // SkinnedMeshRenderer personColor = this.transform.GetComponent<SkinnedMeshRenderer>();
        while (timeStep < seconds)
        {
            // personColor.material.color = new Color(personColor.material.color.r, personColor.material.color.g, personColor.material.color.b, dieCurve.Evaluate(timeStep / 3f));
            if(otherTemp.GetComponentInParent<Obstacle>().deathCounter<=5)
            {
                LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.text = "x" + otherTemp.GetComponentInParent<Obstacle>().deathCounter.ToString();
            }
            else if(otherTemp.GetComponentInParent<Obstacle>().deathCounter > 5 && otherTemp.GetComponentInParent<Obstacle>().deathCounter <= 15)
            {
                LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.text ="NICE"+ "\n"+ "x" + otherTemp.GetComponentInParent<Obstacle>().deathCounter.ToString();

            }
            else if (otherTemp.GetComponentInParent<Obstacle>().deathCounter > 15 && otherTemp.GetComponentInParent<Obstacle>().deathCounter <= 40)
            {
                LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.text = "AWESOME" + "\n" + "x" + otherTemp.GetComponentInParent<Obstacle>().deathCounter.ToString();

            }
            LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.position = new Vector3(LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.position.x, dieCurve.Evaluate(timeStep), LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.position.z);
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].deatCount.transform.gameObject.SetActive(false);
        otherTemp.GetComponentInParent<Obstacle>().deathCounter = 0;
        Die();

    }
    public void Die()
    {
        
        PersonPool.INSTANCE.pool.Push(this);

       
        this.personPrefab.gameObject.SetActiveRecursively(false);
        //this.personPrefab.gameObject.SetActive(false);
        this.personPrefab.transform.SetParent(PersonPool.INSTANCE.transform);
        this.personPrefab.GetComponent<Animator>().enabled = true;
        this.personPrefab.transform.position = Vector3.zero;
        stop = false;
        
    }
    public static void RunAll()
    {
        stopRun = false;
    }
    public void StopPerson()
    {
        stop = true;

    }
    public static void StopAll()
    {
        stopRun = true;
    }
    private IEnumerator FinishGame(float second)
    {

        yield return new WaitForSeconds(second);
        run.SetBool("finish", true);
        StopPerson();
        LevelManager.instance.finishGuys.Add(this);
        if(LevelManager.instance.finishGuys.Count==LevelManager.instance.currentCrowd.gameObject.transform.childCount)
        {
            if(!LevelManager.instance.canNextLevel)
            {
                Debug.Log("Game Over");
                StartCoroutine(LevelManager.instance.LoadSameLevel(1.0f));
            }
           else
           {
                StartCoroutine(LevelManager.instance.NextLevel(2.0f));
           }   
        }
    }
}