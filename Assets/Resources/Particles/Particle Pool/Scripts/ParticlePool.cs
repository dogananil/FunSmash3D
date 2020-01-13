using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [System.NonSerialized] public static ParticlePool instance;
    [SerializeField] public ParticleSystem splashSystem;
    [SerializeField] public ParticleSystem tearSystem;
    [SerializeField] public ParticleSystem confettiSystem;
    [SerializeField] public ParticleSystem confettiTrailSystem;
    [SerializeField] public ParticleSystem popSystem;
    [SerializeField] public ParticleSystem coinSystem;
    [SerializeField] public ParticleSystem rainbowSystem;

    void Awake()
    {
        instance = this;    
    }

    public void PlaySystem(Vector3 position, SYSTEM system, Color color)
    {
        if (!SettingsManager.instance.particlesEnabled)
        {
            return;
        }
        switch (system)
        {
            case SYSTEM.SPLASH_SYSTEM :
                splashSystem.startColor = color;
                splashSystem.transform.position = position;
                splashSystem.Emit(20);
                break;
            case SYSTEM.TEAR_SYSTEM:
                tearSystem.startColor = color;
                tearSystem.transform.position = position;
                tearSystem.Play();
                break;
            case SYSTEM.CONFETTI_SYSTEM:
                confettiSystem.transform.position = position;
                confettiSystem.Play();
                break;
            case SYSTEM.CONFETTI_TRAIL_SYSTEM:
                confettiTrailSystem.transform.position = position;
                confettiTrailSystem.Play();
                break;
            case SYSTEM.POP_SYSTEM:
                popSystem.startColor = color;
                popSystem.transform.position = position;
                popSystem.Play();
                break;
            case SYSTEM.COIN_SYSTEM:
                coinSystem.transform.position = position;
                coinSystem.Play();
                break;
            case SYSTEM.RAINBOW_SYTEM:
                rainbowSystem.transform.position = position;
                rainbowSystem.Play();
                break;
        }
    }

    public void Reset()
    {
        splashSystem.Clear();
        tearSystem.Clear();
        confettiSystem.Clear();
        confettiTrailSystem.Clear();
        popSystem.Clear();
        coinSystem.Clear();
        rainbowSystem.Clear();
    }

    public enum SYSTEM
    {
        SPLASH_SYSTEM, TEAR_SYSTEM, CONFETTI_SYSTEM, CONFETTI_TRAIL_SYSTEM, POP_SYSTEM, COIN_SYSTEM, RAINBOW_SYTEM, JUMP_SYSTEM
    }
}
