using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : MonoBehaviour
{
     private bool slowMotion;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!slowMotion)
        {
            slowMotion = true;
            StartCoroutine(Delay(1f));
            StartCoroutine(SlowMotion());
        }
    }
    public static IEnumerator SlowMotion()
    {
       
        float timeStep = 0f;
        float prevTimeScale = Time.timeScale;
        Time.timeScale = Time.timeScale / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.fixedDeltaTime = Time.fixedDeltaTime / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.maximumDeltaTime= Time.maximumDeltaTime/ LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;

        while (timeStep < 1f)
        {

            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = prevTimeScale;
        Time.fixedDeltaTime = Time.fixedDeltaTime * LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;
        Time.maximumDeltaTime = Time.maximumDeltaTime / LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount - 2].slowMotionSpeed;

    }
    private IEnumerator Delay(float second)
    {
        yield return new WaitForSeconds(second);
    }
}
