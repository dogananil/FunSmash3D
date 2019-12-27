using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
   
    // Start is called before the first frame update
    void Awake()
    {
        INSTANCE = this;
    }
}
