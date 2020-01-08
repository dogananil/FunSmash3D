using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PersonPool personPool;
    [SerializeField] private Crowd crowd;
    [SerializeField] private CrowdPool crowdPool;
    
    // Start is called before the first frame update
    void Awake()
    {
        personPool.InitializePersonPool();
       // crowd.InitializeCrowd();
        //crowdPool.InitializeCrowdPool();
    }
    
    
}
