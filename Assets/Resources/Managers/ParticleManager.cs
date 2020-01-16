using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [System.NonSerialized] public static ParticleManager instance;
    [SerializeField] public ParticleSystem hitSystem;
    [SerializeField] public ParticleSystem deathTrailSystem;

    void Awake()
    {
        instance = this;
    }

    public void PlaySystem(SYSTEM system, Vector3 position, Color color, int emitCount)
    {
        //if (!SettingsManager.instance.particlesEnabled)
        //{
        //    return;
        //}
        ParticleSystem ps = null;
        switch (system)
        {
            case SYSTEM.HIT_SYSTEM:
                ps = hitSystem;
                break;
            case SYSTEM.DEATH_TRAIL:
                ps = deathTrailSystem;
                break;
        }
        if (color != Color.black)
        {
            ps.startColor = color;
        }
        ps.transform.position = position;
        _ = emitCount == 0 ? Play(ps) : Emit(ps, emitCount);
    }

    private int Play(ParticleSystem ps)
    {
        ps.Play();
        return 0;
    }

    private int Emit(ParticleSystem ps, int count)
    {
        ps.Emit(count);
        return 0;
    }

    public void Reset()
    {
        hitSystem.Clear();
        deathTrailSystem.Clear();
    }

    public enum SYSTEM
    {
        HIT_SYSTEM, DEATH_TRAIL
    }
}
