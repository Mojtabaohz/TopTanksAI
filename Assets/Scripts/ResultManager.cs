using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour
{
    public GameObject player01;
    
    public GameObject player02;
    

    public int maxAmmoBox = 2;
    private int spawnedBox = 0 ;
    public GameObject defaultWeapon;

    protected float Timer;
    protected float ChaosTimer;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player01.GetComponent<HealthBar>().alive || !player02.GetComponent<HealthBar>().alive){//base was here
            EndGame();
        }
        
        if(!player01.GetComponent<HealthBar>().alive || !player02.GetComponent<HealthBar>().alive){
            Respawning();
        }

        //SpawnBox();
        //ChaosSpawner();
        
    }

    void EndGame(){
        
    }
    
    void Respawning(){
        if(!player01.GetComponent<HealthBar>().alive){
            //Respawn(player01, player01RP);
        }
        if(!player02.GetComponent<HealthBar>().alive){
            //Respawn(player02, player02RP);
        }
    }

    void Respawn(GameObject obj, Vector3 RP){
        obj.GetComponent<HealthBar>().alive = true;
        obj.GetComponent<shooting>().loaded = false;
        obj.GetComponent<shooting>().shootSign.SetActive(false);
        obj.GetComponent<Transform>().position = RP;
        obj.GetComponent<HealthBar>().SetHealth(obj.GetComponent<HealthBar>().maxHealth);
        obj.SetActive(true);

    }


    public void PickUpBox(){
        spawnedBox -= 1;
    }

    public void SpawnBox(){
        if(spawnedBox <= maxAmmoBox ){
        Timer += Time.deltaTime;
            //if(Timer >= spawnRate){
            //    Timer = 0;
            //    spawnedBox += 1;
            //    Instantiate(RandomBox(),RandomPos(),Quaternion.identity);
            //}
        }
    }
    public void ChaosSpawner(){
        ChaosTimer += Time.deltaTime;
            //if(ChaosTimer >= chaosrate){
            //    GameObject tempChaos = Chaos[Random.Range(0,Chaos.Length)];
            //    ChaosTimer = 0;
            //    Instantiate(tempChaos,RandomPos(),Quaternion.identity);
            //}
    }
}
