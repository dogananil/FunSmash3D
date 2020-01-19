using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    //[SerializeField] public PersonPool pool;
    [SerializeField] public List<Person> crowd = new List<Person>();
    [SerializeField] private GameObject crowdPrefab;
    [SerializeField] private AnimationCurve scaleCurve;
    public void InitializeCrowd(int size,float speedMin,float speedMax)
    {
        int redPersonNumber;
        int modRed;
        for (int i = 0; i < size; i++)
        {
            if (PersonPool.INSTANCE.ListIsEmpty())
            {
                continue;
            }
            crowd.Add(PersonPool.INSTANCE.pool.Pop());

            crowd[i].personPrefab.gameObject.SetActiveRecursively(true);
            crowd[i].personBoomEffect.Stop();
            crowd[i].transform.localScale = Vector3.one * 0.6f;
            crowd[i].transform.GetComponent<SkinnedMeshRenderer>().material.color = crowd[i].color;
            crowd[i].personBoomEffect.GetComponent<Renderer>().material.color = crowd[i].color;
            crowd[i].personBoomEffect.startSize = 1.0f;
            redPersonNumber = (int)(LevelManager.instance.levelProperties.percentageRedPerson * size);
            modRed = size / redPersonNumber;
            if ((i+1)%modRed==0)
            {
                StartCoroutine(ScaleRoutine(crowd[i].transform));
                crowd[i].personBoomEffect.GetComponent<Renderer>().material.color = Color.red;
                crowd[i].personBoomEffect.startSize = 2.0f;
            }
            crowd[i].speed = UnityEngine.Random.Range(speedMin, speedMax);
            crowd[i].GetComponent<BoxCollider>().enabled = true;
            crowd[i].personPrefab.transform.SetParent(LevelManager.instance.currentCrowd.transform);
            crowd[i].dead = false;
        }
    }
    private IEnumerator ScaleRoutine(Transform child)
    {

        float timeStep = 0.0f;
        while (timeStep <= 1.0f)
        {

            child.localScale = Vector3.one * 0.6f * scaleCurve.Evaluate(timeStep);
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        child.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
    }

}
