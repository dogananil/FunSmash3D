using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;
                     private Vector3 start = Vector3.zero;
    [SerializeField] private CrowdPool crowdPool;
    public ObstaclePool obstaclePool;
    public static LevelManager instance;
    public Crowd currentCrowd;
    public List<Obstacle> levelPiece=new List<Obstacle>();
    public List<Obstacle> enemyPieces = new List<Obstacle>();

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
            Obstacle levelBase = Instantiate(obstaclePool.obstaclePool[(int)levelProperties.levelPieces[i].obstacleType], this.transform);
            levelBase.SetObstacleData(levelProperties.levelPieces[i].obstacleSpeed);
            levelPiece.Add(levelBase);
            if (levelProperties.levelPieces[i].obstacleType != Obstacle.TYPE.TYPE0)
            {
                enemyPieces.Add(levelBase.GetComponent<Obstacle>());
            }
            levelBase.transform.localPosition = start;
            start += new Vector3(chillBase.GetComponent<MeshRenderer>().bounds.size.x, 0, 0);
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

    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = Vector3.right * 2.0f;
        for (int i = 0; i < 6; i++)
        {
            Vector3 n = Quaternion.Euler(0, i * 60.0f, 0) * direction;
            Gizmos.DrawLine(Vector3.zero, n);
        }
    }

    private void SetLocationForCrowd()
    {
        currentCrowd.crowd[0].transform.position = Vector3.zero;

        float angle = 0.0f;
        float angleAdder = 72.0f;
        float offset = 0.75f;
        Vector3 direction = Vector3.right;
        Vector3 n = Vector3.zero;

        for (int i = 1; i < currentCrowd.crowd.Count; i++)
        {
            direction = Vector3.right * (offset + Random.Range(-0.3f, 0.3f));

            n = Quaternion.Euler(0, angle + Random.Range(-4.0f, 4.0f), 0) * direction;
            currentCrowd.crowd[i].transform.position = n;

            angle += angleAdder;
            if (angle >= 350.0f)
            {
                angle = 0.0f;
                angleAdder *= 0.55f;
                offset += 1.25f;
            }
        }

    }
}