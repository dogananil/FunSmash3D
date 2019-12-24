using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField] private PersonPool pool;
    [SerializeField] private GameObject crowdPrefab;
    [SerializeField] private int size;
    public void Start()
    {
        for (int i = 0; i < size; i++)
        {
            if (pool.ListIsEmpty())
            {
                return;
            }
            pool.GivePerson().transform.SetParent(this.transform);
        }
    }
    public void Run()
    {
        
    }

    public void Die()
    {
        
    }
}
