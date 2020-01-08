using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Obstacle : MonoBehaviour
{
    //[NonSerialized] public bool activate = false;
    [SerializeField] public TYPE obstacleType;
    [SerializeField] public float obstacleSpeed;
    [SerializeField] public ObstacleData obstacleData;
    public void Activate()
    {
        //  activate = true;
    }
    public void SetObstacleData(float obstacleSpeed)
    {
        this.obstacleSpeed = obstacleSpeed;
    }
    public abstract void Smash();
    public abstract IEnumerator PlayAnimation();
    [Serializable]
    public class ObstacleData
    {
        [SerializeField] public TYPE obstacleType;
        [SerializeField] public float obstacleSpeed;
    }
    public enum TYPE
    {
        TYPE0 = 0, TYPE1 = 1, TYPE2 = 2, TYPE3 = 3, TYPE4 = 4, TYPE5 = 5
    }
}