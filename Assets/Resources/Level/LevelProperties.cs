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
    public Pieces[] levelPieces;
    [Serializable]
    public class Pieces
    {

        public Obstacle.TYPE obstacleType;
        public float obstacleSpeed;
        
    }
}
