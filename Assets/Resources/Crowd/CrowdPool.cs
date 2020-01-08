using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPool : MonoBehaviour
{
    [NonSerialized]public Stack<Crowd> pool = new Stack<Crowd>();
    [SerializeField] private Crowd crowd;
    [SerializeField] private int size;

    public void InitializeCrowdPool()
    {
        for (int i = 0; i < size; i++)
        {
            Crowd newCrowd = Instantiate(crowd,this.transform);
            newCrowd.transform.gameObject.SetActive(false);
            pool.Push(newCrowd);
        } 
    }
    public Crowd GiveCrowd()
    {
        InitializeCrowdPool();
        return pool.Pop();
    }
}
