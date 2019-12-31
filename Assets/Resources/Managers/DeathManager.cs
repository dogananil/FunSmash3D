using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("DeathBase"))
        {
            Debug.Log("Die");
            this.transform.parent.gameObject.SetActive(false);
            this.transform.parent.SetParent(LevelManager.instance.currentCrowd.pool.transform);
            this.transform.parent.position = Vector3.zero;
        }
    }
}
