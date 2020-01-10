using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TabController : MonoBehaviour
{
    public static TabController INSTANCE;
    [NonSerialized]public int tabCount;
    
    [NonSerialized]private bool waitForCrowd;
    public bool run;
    private void Awake()
    {
        INSTANCE = this;
    }

    private void Update()
    {
        Tab();
    }
    private void Tab()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (tabCount > LevelManager.instance.enemyPieces.Count) return;
            if(tabCount==0)
            {
                Person.RunAll();
                run = true;

            }
            else
            {
                LevelManager.instance.enemyPieces[tabCount-1].GetComponent<Obstacle>().Smash();
               
            }

            tabCount++;
        }
    }
}
