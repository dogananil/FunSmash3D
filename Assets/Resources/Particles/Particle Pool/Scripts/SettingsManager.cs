using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [System.NonSerialized] public static SettingsManager instance;
    [SerializeField] public bool canVibrate = true;
    [SerializeField] public bool particlesEnabled = true;

    void Awake()
    {
        instance = this;
    }
}
