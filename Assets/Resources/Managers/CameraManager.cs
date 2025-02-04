﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //void Update()
    //{
    //    this.transform.position = start + new Vector3(LevelManager.instance.currentCrowd.transform.position.x,0f,0f);
    //}

    //[SerializeField] private Transform followObject;
    public static CameraManager INSTANCE;
    [SerializeField] public Vector3 followOffset;
    [SerializeField] public float followSpeed;
    [System.NonSerialized] private float realtimeFollowSpeed;
    [System.NonSerialized] private Vector3 startPosition;
    [System.NonSerialized] public bool canFollow = true;
    [System.NonSerialized] public Vector3 targetPosition = Vector3.zero;
    [System.NonSerialized] public Vector3 shakeOffset = Vector3.zero;
    [System.NonSerialized] public Coroutine shakeRoutine = null;
    [SerializeField] public AnimationCurve shakeCurve;
    [SerializeField] public AnimationCurve panCurve;
    [System.NonSerialized] public Vector3 panOffset = Vector3.zero;
    private Transform currentFrontPerson;
    public bool startCameraBool;
   
    
    void Awake()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
        realtimeFollowSpeed = followSpeed;
        INSTANCE = this;
        
    }

    private void Start()
    {
        //PanOffset(-15.0f, -60.0f, 1.0f, new Vector3(17.0f, 0.0f, -12.0f));
        currentFrontPerson = LevelManager.instance.currentCrowd.transform.GetChild(0).transform;
    }

    void FixedUpdate()
    {
        if (LevelManager.instance.currentCrowd != null )
        {


            realtimeFollowSpeed = Mathf.Lerp(realtimeFollowSpeed, followSpeed, Time.deltaTime * 1.0f);
            if (canFollow)
            {
                //targetPosition = Vector3.Lerp(targetPosition, followObject.position + followOffset, Time.deltaTime * realtimeFollowSpeed);
                Vector3 midPoint = FrontChildren(LevelManager.instance.currentCrowd.transform,-0.5f);
                //Vector3 midPoint = MidPointOfChildren(LevelManager.instance.currentCrowd.transform);
                midPoint.z = 0;
                Debug.DrawLine(Vector3.zero, midPoint);
                targetPosition = Vector3.Lerp(targetPosition, midPoint + followOffset + panOffset, Time.deltaTime * realtimeFollowSpeed);
            }
            else
            {
                targetPosition =Vector3.Lerp(targetPosition,LevelManager.instance.enemyPieces[LevelManager.instance.enemyPieces.Count-1].transform.position+followOffset - Vector3.left*5,Time.deltaTime*realtimeFollowSpeed);
            }
            transform.position = targetPosition + shakeOffset;
        }
    }

    //public Vector3 MidPointOfChildren(Transform transform)
    //{
    //    Vector3 mid = Vector3.zero;
    //    foreach (Transform child in transform)
    //    {
    //        if (!child.GetComponent<Person>().dead)
    //        {
    //            mid += child.position;
    //        }
    //    }
    //    mid /= transform.childCount;
    //    return mid;
    //}
    public Vector3 FrontChildren(Transform transform, float offset)
    {
        
        foreach (Transform child in transform)
        {
            if (!child.GetComponent<Person>().dead && currentFrontPerson.position.x < (child.position.x+offset) )
            {
                currentFrontPerson = child; 
            }
        }
        
        

        return currentFrontPerson.position;

    }
    public void Reset()
    {
        transform.position = startPosition;
        targetPosition = startPosition;
        //canFollow = false;
        followSpeed = 1.0f;
        //followOffset = Vector3.up;
    }

    public void Shake(float magnitude, float speed)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(ShakeRoutine(magnitude, speed));
    }

    private IEnumerator ShakeRoutine(float magnitude, float speed)
    {
        shakeOffset = Vector3.zero;
        float timeStep = 0.0f;
        while (timeStep < 1.0f)
        {
            shakeOffset = Vector3.up * shakeCurve.Evaluate(timeStep) * magnitude;
            timeStep += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        shakeOffset = Vector3.zero;
    }

    public void PanOffset(float transformMagnitude, float angleMagnitude, float speed, Vector3 offset)
    {
        panOffset = offset;
        StartCoroutine(PanRoutine(transformMagnitude, angleMagnitude, speed));
    }

    private IEnumerator PanRoutine(float transformMagnitude, float angleMagnitude, float speed)
    {
        Vector3 startAngles = transform.eulerAngles;
        float timeStep = 0.0f;
        while (timeStep < 1.0f)
        {
            transform.eulerAngles = (startAngles + Vector3.up * panCurve.Evaluate(timeStep) * angleMagnitude);
            timeStep += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        transform.eulerAngles = (startAngles + Vector3.up * panCurve.Evaluate(1.0f) * angleMagnitude);

    }

    
}
