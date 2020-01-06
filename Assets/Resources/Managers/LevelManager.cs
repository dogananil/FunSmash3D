using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;
                     private Vector3 start = Vector3.zero;
    [SerializeField] private CrowdPool crowdPool;
    public ObstaclePool obstaclePool;
    public static LevelManager instance;
    public Crowd currentCrowd;
    public List<GameObject> levelPiece=new List<GameObject>();
    public List<GameObject> enemyPieces = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(4);
    }

    public void LoadLevel(int size)
    {
        TextAsset jsonInfo = Resources.Load<TextAsset>("Level/Levels/level_" + level);
        LevelProperties levelProperties = JsonUtility.FromJson<LevelProperties>(jsonInfo.text);

        for (int i = 0; i < levelProperties.levelPieces.Length; i++)
        {
            GameObject levelBase =levelProperties.levelPieces[i].obstacleType == Obstacle.TYPE.TYPE0 ? Instantiate(chillBase, this.transform) : Instantiate(obstaclePool.obstaclePool[(int)levelProperties.levelPieces[i].obstacleType].transform.gameObject, this.transform);
            levelPiece.Add(levelBase);
            if(levelProperties.levelPieces[i].obstacleType != Obstacle.TYPE.TYPE0) enemyPieces.Add(levelBase);
            levelBase.transform.localPosition = start;
            start += new Vector3(chillBase.GetComponent<MeshRenderer>().bounds.size.x,0,0);
        }
        GetCrowd();
    }

    public void GetCrowd()
    {
        currentCrowd = crowdPool.GiveCrowd();
        currentCrowd.transform.SetParent(this.transform);
        currentCrowd.transform.gameObject.SetActive(true);
        currentCrowd.transform.position = Vector3.zero;
        SetLocationForCrowd();
    }

    private void SetLocationForCrowd()
    {
        int cols = (int)Mathf.Sqrt(currentCrowd.crowd.Count);
        int rows = (int)(((int)Mathf.Sqrt(currentCrowd.crowd.Count)) == Mathf.Sqrt(currentCrowd.crowd.Count) ? Mathf.Sqrt(currentCrowd.crowd.Count) : (int)Mathf.Sqrt(currentCrowd.crowd.Count) + 1);
        Vector3 middleOffset= new Vector3(rows*currentCrowd.crowd[0].transform.lossyScale.x/2 -currentCrowd.crowd[0].transform.lossyScale.x/2,0,-1*cols*currentCrowd.crowd[0].transform.lossyScale.z/2 + currentCrowd.crowd[0].transform.lossyScale.z/2);
        Vector3 newPosition=Vector3.zero + new Vector3(0f, 0f,0f) ;
        float rowScaleTemp =newPosition.x;
        float colScaleTemp = newPosition.z;
        for (int i = 1; i <= currentCrowd.crowd.Count; i++)
        {
            currentCrowd.crowd[i-1].transform.position = newPosition +middleOffset ;
            newPosition = new Vector3(((i+1) % cols == 1 ? rowScaleTemp-=currentCrowd.crowd[i-1].transform.lossyScale.x+1 : newPosition.x),  newPosition.y,  ((i+1) % cols == 1 ?  colScaleTemp = 0f  : colScaleTemp += currentCrowd.crowd[0].transform.lossyScale.z+1));
        }
    }
}