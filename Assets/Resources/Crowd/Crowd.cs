using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField] public PersonPool pool;
    [SerializeField] public List<Person> crowd = new List<Person>();
    [SerializeField] private GameObject crowdPrefab;
    [SerializeField] private int size;
    [SerializeField] private Crowd currentCrowd;
    private float speed = 5f;
    private bool stopRun = false;
    public void InitializeCrowd()
    {
        for (int i = 0; i < size; i++)
        {
            if (pool.ListIsEmpty())
            {
                return;
            }
            crowd.Add(pool.GivePerson());
            crowd[i].transform.SetParent(this.transform);
            crowd[i].transform.gameObject.SetActive(true);
           // Debug.Log(crowd.Count);
        }
    }

    private void Update()
    {
        if (currentCrowd == null || stopRun)
        {
            return;
        }
        currentCrowd.transform.position+= Vector3.right*Time.deltaTime*speed;
    }

    public void Run()
    {
       
        currentCrowd = this;
        stopRun = false;
    }

    public void Die()
    {
        
    }
    public void Stop()
    {
        stopRun = true;
    }
}
