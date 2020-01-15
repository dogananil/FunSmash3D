using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    //[SerializeField] public PersonPool pool;
    [SerializeField] public List<Person> crowd = new List<Person>();
    [SerializeField] private GameObject crowdPrefab;
    public void InitializeCrowd(int size,float speedMin,float speedMax)
    {
        
        for (int i = 0; i < size; i++)
        {
            if (PersonPool.INSTANCE.ListIsEmpty())
            {
                continue;
            }

            crowd.Add(PersonPool.INSTANCE.pool.Pop());
            crowd[i].speed = UnityEngine.Random.Range(speedMin, speedMax);
            crowd[i].personPrefab.transform.SetParent(LevelManager.instance.currentCrowd.transform);
            crowd[i].personPrefab.transform.gameObject.SetActive(true);
            crowd[i].dead = false;
        }
    }
   
}
