using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : MonoBehaviour
{
     public static bool slowMotion;
    private Coroutine slowRoutine=null;
   // private float prevTimeScale;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ragdoll"))
        {

            if (!SlowMotionEffect.slowMotion)
            {
                if (slowRoutine != null)
                {
                    StopCoroutine(slowRoutine);
                }
                SlowMotionEffect.slowMotion = true;
            // StartCoroutine(Delay(0.2f));
            
                slowRoutine = StartCoroutine(SlowMotion());
            }
        }
    }
    public IEnumerator SlowMotion()
    {
       //  prevTimeScale = Time.timeScale;
        Time.timeScale = Time.timeScale / LevelManager.instance.currentEnemy.slowMotionSpeed;
        Time.fixedDeltaTime = Time.fixedDeltaTime / LevelManager.instance.currentEnemy.slowMotionSpeed;
        Time.maximumDeltaTime= Time.maximumDeltaTime/ LevelManager.instance.currentEnemy.slowMotionSpeed;

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.fixedDeltaTime * LevelManager.instance.currentEnemy.slowMotionSpeed;
        Time.maximumDeltaTime = Time.maximumDeltaTime / LevelManager.instance.currentEnemy.slowMotionSpeed;
        SlowMotionEffect.slowMotion = false;

    }
    private IEnumerator Delay(float second)
    {
        yield return new WaitForSeconds(second);
        
    }
}
