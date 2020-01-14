using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField] public PersonPool pool;
    [SerializeField] public List<Person> crowd = new List<Person>();
    [SerializeField] private GameObject crowdPrefab;
    public void InitializeCrowd(int size,float speedMin,float speedMax)
    {
        
        for (int i = 0; i < size; i++)
        {
            if (pool.ListIsEmpty())
            {
                return;
            }
            crowd.Add(pool.GivePerson());
            crowd[i].speed = UnityEngine.Random.Range(speedMin, speedMax);
            crowd[i].transform.SetParent(this.transform);
            crowd[i].transform.gameObject.SetActive(true);
        }
    }
   
}
