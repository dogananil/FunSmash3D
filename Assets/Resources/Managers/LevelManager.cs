using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject chillBase;
    [SerializeField] private GameObject obstacle;
                     private Vector3 start = Vector3.zero;
    [SerializeField] private CrowdPool crowdPool;
    // Start is called before the first frame update
    void Start()
    {
        
            CreateLevel(4);
        
        // Debug.Log();
    }

    public void CreateLevel(int size)
    {
        
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
        currentCrowd.transform.SetParent(this.transform);
        currentCrowd.transform.gameObject.SetActive(true);
        currentCrowd.transform.localPosition = Vector3.zero;
        SetLocationForCrowd(currentCrowd);

        Debug.Log(currentCrowd.crowd.Count);
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

        //float boundOffset = (baseArea - (currentCrowd.crowd.Count * personArea)) / 4;
		float ratio = baseArea/(currentCrowd.crowd.Count * personArea) + (currentCrowd.crowd.Count * personArea)/ baseArea;
        int cols = (int)Mathf.Sqrt(currentCrowd.crowd.Count);
        
        int rows = (int)(((int)Mathf.Sqrt(currentCrowd.crowd.Count)) == Mathf.Sqrt(currentCrowd.crowd.Count) ? Mathf.Sqrt(currentCrowd.crowd.Count) : (int)Mathf.Sqrt(currentCrowd.crowd.Count) + 1);
        Debug.Log("Rows::" + rows);
        Debug.Log("Cols::"+cols);
        Debug.Log("Person Height :: " + personHeight);
        Vector3 newPosition = new Vector3(chillBase.transform.position.x - baseWidth / 2, currentCrowd.crowd[0].transform.lossyScale.y / 2, chillBase.transform.position.z - baseHeight / 2);
        float rowScaleTemp =0;
        float colScaleTemp = 0;
        for (int i = 1; i <= currentCrowd.crowd.Count; i++)
        {
            currentCrowd.crowd[i-1].transform.position = newPosition;
        newPosition = new Vector3(((i+1) % cols == 1 ? rowScaleTemp+=currentCrowd.crowd[i-1].transform.lossyScale.x : 0f), 0f,  ((i+1) % cols == 1 ?  colScaleTemp += currentCrowd.crowd[0].transform.lossyScale.z : colScaleTemp = chillBase.transform.position.z - baseHeight / 2));
            Debug.Log("New Position-" + i + " ===" + currentCrowd.crowd[0].transform.lossyScale.z);
        }
        Debug.Log("sadsad");

        //currentCrowd.crowd[0]


    }
}