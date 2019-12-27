using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Person : MonoBehaviour
{
    [SerializeField] public Animator run, die;
    [SerializeField] private GameObject personPrefab;

    private void Update()
    {

        run.SetBool("run", TabController.INSTANCE.run);


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            TabController.INSTANCE.run = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Animator>().enabled = false;
        }
        else if (other.transform.CompareTag("DeathBase"))
        {
            this.transform.gameObject.SetActive(false);
            this.transform.SetParent(LevelManager.instance.currentCrowd.pool.transform);
            this.transform.position = Vector3.zero;
        }
    }
}