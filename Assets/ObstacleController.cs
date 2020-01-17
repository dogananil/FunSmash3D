using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Coroutine scaleRoutine;
    [SerializeField] private AnimationCurve scaleCurve;
    private bool used;
    [SerializeField] private BoxCollider triggerOne;
    [SerializeField] private BoxCollider notTriggerOne;

    private void OnTriggerEnter(Collider other)
{
        if(used)
        {
         
            return;
        }
        if (other.transform.CompareTag("Person"))
        {

            triggerOne.enabled = false;
            notTriggerOne.enabled = true;
            if (LevelManager.instance.currentEnemy != null)
            {
                LevelManager.instance.currentEnemy.MakeTransparent();
                LevelManager.instance.currentEnemy.smash = true;

            }
            if(!this.transform.CompareTag("ChillBase"))
            {
                LevelManager.instance.currentEnemy = this.transform.parent.GetComponent<Obstacle>();
                LevelManager.instance.currentEnemy.MakeOpaque();
            }
            
            if (Person.currentFront != null && Person.currentFront!=other.transform)// Another person trigger
            {
                Person.currentFront.localScale = Vector3.one * 0.6f;
                Person.currentFront.GetComponent<SkinnedMeshRenderer>().material.color = other.transform.GetComponent<SkinnedMeshRenderer>().material.color;
            }

            if(Person.currentFront != other.transform)
            {   
            if (scaleRoutine != null)
            {
                StopCoroutine(scaleRoutine);
                scaleRoutine = null;
            }
            scaleRoutine = StartCoroutine(ScaleRoutine(other.transform));
            
            Person.currentFront = other.transform;
            
            }
            used = true;
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
