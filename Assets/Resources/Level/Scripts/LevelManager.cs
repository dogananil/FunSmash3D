using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel(4);
    }

    public void CreateLevel(int size)
    {
        Vector3 start=Vector3.zero;
        for (int i = 0; i < size; i++)
        {
            GameObject levelBase =
                i % 2 == 0 ? Instantiate(chillBase, this.transform) : Instantiate(obstacle, this.transform);
            levelBase.transform.localPosition = start;
            start += new Vector3(chillBase.GetComponent<MeshRenderer>().bounds.size.x,0,0);
        }
    }
}