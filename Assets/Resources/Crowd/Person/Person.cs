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
        
            run.SetBool("run",TabController.INSTANCE.run);
            
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Debug.Log("Box");
            TabController.INSTANCE.run = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Animator>().enabled = false;
        }

    }
}
