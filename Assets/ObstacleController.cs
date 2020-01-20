using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Coroutine scaleRoutine;
  
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
           /* if (LevelManager.instance.currentEnemy != null)
            {
               

            }*/
            if(!this.transform.CompareTag("ChillBase"))
            {
                if(LevelManager.instance.currentEnemy!=null)
                {
                    LevelManager.instance.currentEnemy.MakeTransparent();
                    LevelManager.instance.currentEnemy.smash = true;
                    LevelManager.instance.currentEnemy.obstacleParticle.SetActive(false);

                }
                LevelManager.instance.currentEnemy = this.transform.parent.GetComponent<Obstacle>();
                LevelManager.instance.currentEnemy.MakeOpaque();
                LevelManager.instance.currentEnemy.obstacleParticle.SetActive(true);

            }

            /*if (Person.currentFront != null && Person.currentFront!=other.transform)// Another person trigger
            {
                Person.currentFront.localScale = Vector3.one * 0.6f;
                Person.currentFront.GetComponent<SkinnedMeshRenderer>().material.color = other.transform.GetComponent<SkinnedMeshRenderer>().material.color;
            }*/
            used = true;
        }
}
    

}
