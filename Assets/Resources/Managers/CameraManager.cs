using System;
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
    [SerializeField] public Vector3 followOffset;
    [SerializeField] public float followSpeed;
    [System.NonSerialized] private float realtimeFollowSpeed;
    [System.NonSerialized] private Vector3 startPosition;
    [System.NonSerialized] public bool canFollow = true;
    [System.NonSerialized] public Vector3 targetPosition = Vector3.zero;
    [System.NonSerialized] public Vector3 shakeOffset = Vector3.zero;
    [System.NonSerialized] public Coroutine shakeRoutine = null;
    [SerializeField] public AnimationCurve shakeCurve;

    void Awake()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
        realtimeFollowSpeed = followSpeed;
    }

    void FixedUpdate()
    {
        realtimeFollowSpeed = Mathf.Lerp(realtimeFollowSpeed, followSpeed, Time.deltaTime * 1.0f);
        if (canFollow)
        {
            //targetPosition = Vector3.Lerp(targetPosition, followObject.position + followOffset, Time.deltaTime * realtimeFollowSpeed);
            targetPosition = Vector3.Lerp(targetPosition, LevelManager.instance.currentCrowd.transform.position + followOffset, Time.deltaTime * realtimeFollowSpeed);
        }
        else
        {
            targetPosition = Vector3.Lerp(targetPosition, Vector3.up * 2.0f, Time.deltaTime * realtimeFollowSpeed);
        }
        transform.position = targetPosition + shakeOffset;
    }

    public void Reset()
    {
        transform.position = startPosition;
        targetPosition = startPosition;
        canFollow = false;
        followSpeed = 1.0f;
        followOffset = Vector3.up;
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
}
