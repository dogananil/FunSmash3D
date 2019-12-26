using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TabController : MonoBehaviour
{
    private int tabCount = 1;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tabCount > LevelManager.instance.levelPiece.Count) return;
            LevelManager.instance.levelPiece[tabCount].GetComponent<Obstacle>().Smash();
            tabCount += 2;
        }
    }
}
