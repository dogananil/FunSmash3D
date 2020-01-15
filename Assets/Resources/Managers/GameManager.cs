using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
   
    // Start is called before the first frame update
    void Awake()
    {
        INSTANCE = this;
        GameAnalytics.Initialize();
        Application.targetFrameRate = 60;
    }
}
