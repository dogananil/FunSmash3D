using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;

    [SerializeField] private CrowdPool crowdPool;
    // Start is called before the first frame update
    void Start()
    {
        
            CreateLevel(4);
        
        // Debug.Log();
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
       
        GetCrowd();
       
    }

    public void GetCrowd()
    {
        Crowd currentCrowd = crowdPool.GiveCrowd();
        SetLocationForCrowd(currentCrowd);
        //Debug.Log(currentCrowd.crowd.Count);
    }

    private void SetLocationForCrowd(Crowd currentCrowd)
    {
        float baseWidth = chillBase.GetComponent<MeshRenderer>().bounds.size.x;
        float baseHeight = chillBase.GetComponent<MeshRenderer>().bounds.size.z;
        float baseArea = baseWidth * baseHeight;
        float personWidth = currentCrowd.crowd[0].GetComponent<MeshRenderer>().bounds.size.x;
        float personHeight = currentCrowd.crowd[0].GetComponent<MeshRenderer>().bounds.size.z;
        float personDepth = currentCrowd.crowd[0].GetComponent<MeshRenderer>().bounds.size.y;
        float personArea = personHeight * personWidth;

        float boundOffset = (baseArea - (currentCrowd.crowd.Count * personArea)) / 4;
        Vector3 newPosition = new Vector3(chillBase.transform.position.x-baseWidth/2,chillBase.transform.position.z-baseHeight/2,0f);
        for (int i = 0; i < currentCrowd.crowd.Count; i++)
        {
            currentCrowd.crowd[i].transform.position=newPosition + new Vector3(boundOffset,boundOffset,personDepth/2);
           // newPosition= currentCrowd.crowd[i].transform.position + new Vector3(personWidth,)
        }
        
        //currentCrowd.crowd[0]
        
    }
}