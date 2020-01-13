using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Person : MonoBehaviour
{
    [SerializeField] public Animator run, finish;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private AnimationCurve dieCurve;
    public float speed;
    private static bool stopRun = false;

    [SerializeField] private Color color;
    [System.NonSerialized] private MeshRenderer meshRenderer;
    [System.NonSerialized] public bool dead = false;
    private bool stop = false;


    private void Update()
    {
        if (this == null || stopRun || !TabController.INSTANCE.run || stop)
        {
            return;
        }
        this.transform.position += Vector3.right * Time.deltaTime * speed;
        //Debug.Log(speed);
        run.SetBool("run", TabController.INSTANCE.run);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            dead = true;
            this.GetComponent<Animator>().enabled = false;
           
            this.transform.SetParent(LevelManager.instance.currentCrowd.pool.transform);
            ParticleManager.instance.PlaySystem(ParticleManager.SYSTEM.HIT_SYSTEM, transform.position, color, 20);
            ScrollBar.INSTANCE.LoadProgessBar();
            StopPerson();
            StartCoroutine(DieSlowly(6.0f));

        }
        else if (other.transform.CompareTag("DeathBase"))
        {
            Die();
        }
        else if(other.transform.CompareTag("FinishBase"))
        {
            StartCoroutine(FinishGame(this.speed/2f));
            
        }
    }

    private IEnumerator DieSlowly(float seconds)
    {

        float timeStep = 0f;
       // SkinnedMeshRenderer personColor = this.transform.GetComponent<SkinnedMeshRenderer>();
        while (timeStep < seconds)
        {
           // personColor.material.color = new Color(personColor.material.color.r, personColor.material.color.g, personColor.material.color.b, dieCurve.Evaluate(timeStep / 3f));
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Die();

    }
    private void Die()
    {

        this.transform.gameObject.SetActive(false);
        this.transform.SetParent(LevelManager.instance.currentCrowd.pool.transform);
        this.transform.position = Vector3.zero;
        
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
                StartCoroutine(LevelManager.instance.NextLevel(4.0f));
            }
            
        }
    }
}