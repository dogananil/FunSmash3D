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
    private bool confetiBool;
    [SerializeField] private Image skullIcon;
    [SerializeField] private Text needToKill;
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
        if(percantageLevelFinish.value<=slider.value && !confetiBool)
        {
            skullIcon.color = Color.green;
            ParticlePool.instance.PlaySystem(LevelManager.instance.currentEnemy.transform.position, ParticlePool.SYSTEM.CONFETTI_SYSTEM, Color.black);
            //ParticlePool.instance.PlaySystem(LevelManager.instance.enemyPieces[TabController.INSTANCE.tabCount-2].transform.position, ParticlePool.SYSTEM.CONFETTI_TRAIL_SYSTEM, Color.black);
            confetiBool = true;
            LevelManager.instance.canNextLevel = true;
            needToKill.color = Color.green;
        }
    }
    public void StartProgressBar()
    {
        deathCount.text = ((float)LevelManager.instance.levelProperties.crowdSize - LevelManager.instance.currentCrowd.transform.childCount) + " / " + LevelManager.instance.levelProperties.crowdSize;
        levelText.text = (LevelManager.instance.level+1).ToString();
        percantageLevelFinish.value = LevelManager.instance.levelProperties.percantageLevelFinish;
        needToKill.text = "x" + (int)(LevelManager.instance.levelProperties.crowdSize * LevelManager.instance.levelProperties.percantageLevelFinish);
        
    }
    public void ResetProgressBar()
    {
        slider.value = 0;
        confetiBool = false;
        needToKill.color = Color.white;
        skullIcon.color = Color.white;

    }
}
