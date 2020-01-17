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
}
public enum BlendMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent
}