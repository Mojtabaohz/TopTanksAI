using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager :MonoBehaviour{
  private static Manager instance = null;
  public static Manager Instance{
    get{
      if(instance == null){
        instance = FindObjectOfType<Manager>();
        if(instance==null){
          GameObject go = new GameObject();
          go.name="Manager";
          instance = go.AddComponent<Manager>();
          DontDestroyOnLoad(go);
        }
      }
      return instance;
    }
    }
  public Text result;
  void Awake(){
    if(instance == null){
      instance = this;
      DontDestroyOnLoad(this.gameObject);
    }
    else{
      Destroy(gameObject);
    }
      
  }
}


    

