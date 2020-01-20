using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class Obstacle : MonoBehaviour
{
    //[NonSerialized] public bool activate = false;
    [SerializeField] public TYPE obstacleType;
    [SerializeField] public float obstacleSpeed;
    [SerializeField] public ObstacleData obstacleData;
    [SerializeField] public float slowMotionSpeed;
    [SerializeField] public TextMeshPro deatCount;
    [SerializeField]public static int deathCounter = 0;
     public GameObject obstacleParticle;

    public bool smash;
    public AnimationCurve textCurve;

 

    public void Activate()
    {
        //  activate = true;
    }
    public void SetObstacleData(float obstacleSpeed,float slowMotionSpeed)
    {
        this.obstacleSpeed = obstacleSpeed;
        this.slowMotionSpeed = slowMotionSpeed;
    }
    public abstract void Smash();
    public abstract IEnumerator PlayAnimation();
    [Serializable]
    public class ObstacleData
    {
        [SerializeField] public TYPE obstacleType;
        [SerializeField] public float obstacleSpeed;
        [SerializeField] public float slowMotionSpeed;
    }
    public enum TYPE
    {
        TYPE0 = 0, TYPE1 = 1, TYPE2 = 2, TYPE3 = 3, TYPE4 = 4, TYPE5 = 5, TYPE6=6
    }
    public abstract void MakeTransparent();
    public abstract void MakeOpaque();
    
    public void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_OFF");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }

    }
    public void ScoreToString()
    {
        if(Obstacle.deathCounter==0)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "So Close";
        }
        else if (Obstacle.deathCounter <= 5)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "x" + Obstacle.deathCounter.ToString() + " Combo";
        }
        else if (Obstacle.deathCounter > 5 && Obstacle.deathCounter <= 15)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "NICE" + "\n" + "x" + Obstacle.deathCounter.ToString() + " Combo";

        }
        else if (Obstacle.deathCounter > 15 && Obstacle.deathCounter <= 40)
        {
            LevelManager.instance.currentEnemy.deatCount.text = "PERFECT" + "\n" + "x" + Obstacle.deathCounter.ToString() + " Combo";

        }
        
        else
        {
            LevelManager.instance.currentEnemy.deatCount.text = "AWESOME" + "\n" + "x" + Obstacle.deathCounter.ToString() + " Combo";

        }
    }
    public IEnumerator TextAnimation()
    {
        Vector3 startPosition = LevelManager.instance.currentEnemy.transform.position;
        this.deatCount.transform.gameObject.SetActive(true);
        this.deatCount.transform.forward = Camera.main.transform.forward;
        ScoreToString();

        Obstacle.deathCounter = 0;

        float timeStep = 0f;

        while (timeStep < 5)
        {

            this.deatCount.transform.position = startPosition + new Vector3(0, textCurve.Evaluate(timeStep), 2.0f);
            timeStep += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        this.deatCount.transform.gameObject.SetActive(false);
    }
}
public enum BlendMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent
}