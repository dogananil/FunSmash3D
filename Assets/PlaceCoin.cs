using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCoin : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int coinNumber; 
    private Vector3 start = new Vector3(0f,0.5f,0f);
    // Start is called before the first frame update
    void Start()
    {// 9-15
         for(int i=0;i<9;i++)
        {
            for(int j=0;j<15;j++)
            {
                
                GameObject coinClone = Instantiate(coin, this.transform);
                coinClone.transform.localPosition = j /8 == 0 ?  start + new Vector3(j, 0, i): start + new Vector3(-(j % 8)-1, 0, i);
                LevelManager.instance.coins.Add(coinClone);
            }
            
        }
    }

    
}
