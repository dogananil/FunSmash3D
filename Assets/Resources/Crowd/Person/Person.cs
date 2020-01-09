using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Person : MonoBehaviour
{
    [SerializeField] public Animator run, die;
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
            StopPerson();


        }
        else if (other.transform.CompareTag("DeathBase"))
        {
            Die();
        }
    }

    /* private IEnumerator DieSlowly()
    {
        Debug.Log("DieSlowly");

        float timeStep =0f;
        SkinnedMeshRenderer personColor = this.transform.GetComponent<SkinnedMeshRenderer>();
        while(timeStep<3f)
        {
            personColor.material.color = new Color(personColor.material.color.r,personColor.material.color.g,personColor.material.color.b,dieCurve.Evaluate(timeStep/3f));
            timeStep += Time.deltaTime;
            Debug.Log("PersonColor********* Time Step ==== " + personColor.material.color.a);
            yield return new WaitForEndOfFrame();
        }

    }*/
    private void Die()
    {
        Debug.Log("Die");
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
}