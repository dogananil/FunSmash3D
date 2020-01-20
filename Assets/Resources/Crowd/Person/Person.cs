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

    [SerializeField] public Color color;
    [System.NonSerialized] private MeshRenderer meshRenderer;
    [System.NonSerialized] public bool dead = false;
    [System.NonSerialized] public bool dead2 = false;
    private bool stop = false;
    private Collider otherTemp;
     public ParticleSystem personBoomEffect;
    public static Transform currentFront;



    private void Update()
    {
        if (this == null || stopRun || !TabController.INSTANCE.run || stop)
        {
            return;
        }
        this.personPrefab.transform.position += Vector3.right * Time.deltaTime * speed;
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
            
            Obstacle.deathCounter++;

            otherTemp = other;
            ScrollBar.INSTANCE.LoadProgessBar();
            StopPerson();
            StartCoroutine(DieSlowly(3.0f));

        }
        else if (other.transform.CompareTag("FinishBase"))
        {
            dead = true;
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            this.personPrefab.transform.SetParent(PersonPool.INSTANCE.transform);
            CameraManager.INSTANCE.canFollow = false;


            BombPerson();
            Die();

        }
        else if(other.transform.CompareTag("End"))
        {
            dead = true;
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            this.personPrefab.transform.SetParent(PersonPool.INSTANCE.transform);
            Die();
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
        if (LevelManager.instance.finishGuys.Count == LevelManager.instance.currentCrowd.gameObject.transform.childCount)
        {
            if (!LevelManager.instance.canNextLevel)
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
    private void BombPerson()
    {
        float power = 5f;
        float radius = 2.0f;
        float upforce = 1.0f;
        this.personBoomEffect.Play(true);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rig = hit.GetComponent<Rigidbody>();
            if(rig!=null &&hit.CompareTag("FinishBase"))
            {
                rig.AddExplosionForce(power*this.transform.localScale.magnitude, this.transform.position, radius, upforce, ForceMode.Impulse);
                hit.enabled = false;
                LevelManager.instance.coins.Remove(hit.gameObject);
                Destroy(hit.transform.gameObject,1.0f);
                if (LevelManager.instance.coins.Count == 0)
                {
                    LevelManager.instance.goToNextLevel = true;
                    LevelManager.instance.GameOver(1.0f);
                    return;
                }
            }
            
        }
        
    }
}