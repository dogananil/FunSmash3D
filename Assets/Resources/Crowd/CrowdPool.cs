using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPool : MonoBehaviour
{
    [SerializeField] public List<Crowd> pool = new List<Crowd>();
    [SerializeField] private Crowd crowd;
    [SerializeField] private int size;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            Crowd newCrowd = Instantiate(crowd,this.transform);
        } 
    }
    
}
