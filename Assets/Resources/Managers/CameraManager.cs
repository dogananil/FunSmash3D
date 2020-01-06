using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 start;

    private void Start()
    {
        start = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = start +new Vector3(LevelManager.instance.currentCrowd.transform.position.x,0f,0f);
    }
    
}
