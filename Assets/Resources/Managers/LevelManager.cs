using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private PersonPool personPool;
    private Vector3 start = Vector3.zero;
    [SerializeField] private CrowdPool crowdPool;
    public ObstaclePool obstaclePool;
    public static LevelManager instance;
    public Crowd currentCrowd;
    [NonSerialized]public List<Obstacle> levelPiece=new List<Obstacle>();
    public List<Obstacle> enemyPieces = new List<Obstacle>();
    public List<Person> finishGuys = new List<Person>();
    public LevelProperties levelProperties;
    public bool canNextLevel;
    private bool goToNextLevel; 

    private void Awake()
    {
        instance = this;
    }

    public int level;
    // Start is called before the first frame update
    void Start()
    {
        personPool.InitializePersonPool();
        level =PlayerPrefs.GetInt("Level");
        LoadLevel(level);
    }
    private void Update()
    {
        if(currentCrowd.transform.childCount==0 && !goToNextLevel)
        {
            goToNextLevel = true;
            StartCoroutine(LevelManager.instance.NextLevel(2.0f));
        }
    }
    public void CreateLevel(int size,LevelProperties levelProperties)
    {
       
        for (int i = 0; i < levelProperties.levelPieces.Length; i++)
        {
            Obstacle levelBase = Instantiate(obstaclePool.obstaclePool[(int)levelProperties.levelPieces[i].obstacleType], this.transform);
            levelBase.SetObstacleData(levelProperties.levelPieces[i].obstacleSpeed,levelProperties.levelPieces[i].slowMotionSpeed);
            levelPiece.Add(levelBase);
            if (levelProperties.levelPieces[i].obstacleType != Obstacle.TYPE.TYPE0)
            {
                enemyPieces.Add(levelBase.GetComponent<Obstacle>());
            }
            levelBase.transform.localPosition = start;
            start += new Vector3(chillBase.GetComponent<MeshRenderer>().bounds.size.x, 0, 0);
        }
        GetCrowd(levelProperties.crowdSize);
    }
    public void LoadLevel(int level)
    {
        TextAsset jsonInfo = Resources.Load<TextAsset>("Level/Levels/level_" + level);
        levelProperties = JsonUtility.FromJson<LevelProperties>(jsonInfo.text);
       
        CreateLevel(levelProperties.crowdSize, levelProperties);
    }
    public void GetCrowd(int size)
    {
       
        currentCrowd.InitializeCrowd(size,levelProperties.crowdMinSpeed,levelProperties.crowdMaxSpeed);
        //currentCrowd = crowdPool.GiveCrowd();
        currentCrowd.transform.SetParent(this.transform);
        currentCrowd.transform.gameObject.SetActive(true);
        currentCrowd.transform.position = Vector3.zero;
        ScrollBar.INSTANCE.StartProgressBar();
        SetLocationForCrowd();
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
    public IEnumerator NextLevel(float second)
    {
        yield return new WaitForSeconds(second);
        DestroyAll();
        LoadLevel(++level);
        PlayerPrefs.SetInt("level", level);
        
    }
    public IEnumerator LoadSameLevel(float second)
    {
        yield return new WaitForSeconds(second);
        DestroyAll();
        LoadLevel(level);
        PlayerPrefs.SetInt("level", level);

    }
    private void DestroyAll()
    {
        for(int i=0;i<this.transform.childCount;i++)
        {
            if(this.transform.GetChild(i).gameObject.name=="Crowd")
            {
                currentCrowd.transform.SetParent(crowdPool.transform);
                if(currentCrowd.transform.childCount!=0)
                {
                    
                    while (currentCrowd.transform.childCount!=0)
                    {
                        currentCrowd.transform.GetChild(0).transform.GetComponent<Person>().Die();
                    }
                }
                
            }
            else
            {
                Destroy(this.transform.GetChild(i).transform.gameObject);
            }

        }
        ResetAll();
    }
    private void ResetAll()
    {
        enemyPieces.Clear();
        start = Vector3.zero;
        finishGuys.Clear();
        currentCrowd.crowd.Clear();
        canNextLevel = false;
        TabController.INSTANCE.tabCount = 0;
        Person.StopAll();
        ScrollBar.INSTANCE.ResetProgressBar();
        goToNextLevel = false;
    }
}