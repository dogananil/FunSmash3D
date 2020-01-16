using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TabController : MonoBehaviour
{
    public static TabController INSTANCE;
    public int tabCount;
    
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

            
            if(tabCount==0)
            {
                Person.RunAll();
                run = true;

            }
            else if(tabCount!=0 && !LevelManager.instance.currentEnemy.smash)
            {
                LevelManager.instance.currentEnemy.Smash();
                LevelManager.instance.currentEnemy.smash = true;


            }

            tabCount++;
        }
    }
}
