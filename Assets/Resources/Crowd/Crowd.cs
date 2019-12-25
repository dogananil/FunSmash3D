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
           // Debug.Log(crowd.Count);
        }
    }
    public void Run()
    {
        
    }

    public void Die()
    {
        
    }
}
