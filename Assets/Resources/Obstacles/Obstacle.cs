using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle:MonoBehaviour
{
    [NonSerialized] public bool activate = false;
    [SerializeField] public TYPE type;


    public void Activate()
    {
        activate = true;
    }

    public abstract void Smash();
    public abstract IEnumerator PlayAnimation();
    
    public enum  TYPE
    {
        TYPE1=1,TYPE2=2,TYPE3=3,TYPE4=4,TYPE5=5
    }
}
