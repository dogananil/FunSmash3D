using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TabController : MonoBehaviour
{
    public static TabController INSTANCE;
    [NonSerialized]private int tabCount;
    
    [NonSerialized]private int chillBaseIndex;
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
        if (LevelManager.instance.currentCrowd.transform.position.x > LevelManager.instance.levelPiece[chillBaseIndex].transform.position.x)
        {
            LevelManager.instance.currentCrowd.Stop();
            run = false;
            waitForCrowd = false;

        }
        if (Input.GetMouseButtonDown(0))
        {

            if (tabCount > LevelManager.instance.levelPiece.Count || waitForCrowd) return;

            if (tabCount % 2 == 1)
            {
                LevelManager.instance.levelPiece[tabCount].GetComponent<Obstacle>().Smash();
                waitForCrowd = true;
            }
            else
            {
                LevelManager.instance.currentCrowd.Run();
                run = true;
                chillBaseIndex += 2;
               
            }
            //(tabCount % 2 == 1 ? (Action)LevelManager.instance.levelPiece[tabCount].GetComponent<Obstacle>().Smash : LevelManager.instance.currentCrowd.Run)();

            tabCount++;
        }
    }
}
