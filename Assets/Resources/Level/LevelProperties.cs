using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelProperties
{
    [SerializeField] 
    public string crowdColor;
    public int crowdSize;
    public float crowdMinSpeed;
    public float crowdMaxSpeed;
    public Obstacle.ObstacleData[] levelPieces;
   
}
