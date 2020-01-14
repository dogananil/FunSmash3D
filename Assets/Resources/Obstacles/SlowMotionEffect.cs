using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : MonoBehaviour
{
     private bool slowMotion;
    private Coroutine slowRoutine=null;
   // private float prevTimeScale;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!slowMotion)
        {
            if(slowRoutine!=null)
            {
                StopCoroutine(slowRoutine);
            }
                slowMotion = true;
                // StartCoroutine(Delay(0.2f));
               slowRoutine= StartCoroutine(SlowMotion());
            
        }
    }
    public IEnumerator SlowMotion()
    {
       //  prevTimeScale = Time.timeScale;
        Time.timeScale = Time.timeScale / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.fixedDeltaTime = Time.fixedDeltaTime / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.maximumDeltaTime= Time.maximumDeltaTime/ LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.fixedDeltaTime * LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.maximumDeltaTime = Time.maximumDeltaTime / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;

    }
    private IEnumerator Delay(float second)
    {
        yield return new WaitForSeconds(second);
        
    }
}
