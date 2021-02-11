using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [Header("Detected Enemies")]
    [SerializeField]
    public List<GameObject> spottedEnemies = new List<GameObject>();
    
    public void UpdateEnemyList(GameObject obj){
        spottedEnemies.Add(obj);
        foreach(Transform child in transform){
            child.GetComponent<ControllerAI>().UpdateSelfEnemyList(obj);
            child.GetComponent<ControllerAI>().SortList();
        }
        
    }
    
    
     
}
