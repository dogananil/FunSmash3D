using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollBar : MonoBehaviour
{
    private  Slider slider;
    public TextMeshProUGUI deathCount;
    public static ScrollBar INSTANCE;
    public TextMeshProUGUI levelText;
    public Slider percantageLevelFinish;
    private void Awake()
    {
        INSTANCE = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();

    }

    public void LoadProgessBar()
    {
        slider.value = (float)((float)LevelManager.instance.levelProperties.crowdSize-LevelManager.instance.currentCrowd.transform.childCount) /(float)LevelManager.instance.levelProperties.crowdSize;
        deathCount.text = ((float)LevelManager.instance.levelProperties.crowdSize - LevelManager.instance.currentCrowd.transform.childCount) + " / " + (float)LevelManager.instance.levelProperties.crowdSize;
    }
    public void StartProgressBar()
    {
        deathCount.text = ((float)LevelManager.instance.levelProperties.crowdSize - LevelManager.instance.currentCrowd.transform.childCount) + " / " + LevelManager.instance.levelProperties.crowdSize;
        levelText.text = (LevelManager.instance.level+1).ToString();
        percantageLevelFinish.value = LevelManager.instance.levelProperties.percantageLevelFinish;
    }
    public void ResetProgressBar()
    {
        slider.value = 0;
    }
}
