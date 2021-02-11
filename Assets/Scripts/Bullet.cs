using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public int bulletSpeed = 1000;
    public int dmg =  5;
    public bool explosive = false;
    public float explosionPower = 10.0f;
    public float radius = 5.0f;
    public bool collisionEnable = false;
    public GameObject bigExplosionPrefab;
    //public bool damageOverTime = false;
    //public float damageInterval = 1f;
    //public float damageOverTimeDuration = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        //if(explosive & (bullet == enabled)){
        //    Invoke("Detonation",5);
        //}
    }

    void OnTriggerEnter(Collider other){
        
        if(collisionEnable & !explosive){
            if(other.gameObject.tag.Equals("Player")){
                DoDamage(dmg,other);
                //Debug.Log("Damage Done");
                gameObject.SetActive(false);
                
            }
        }
        //DoDamage(damage,other);
        
    }


    void DoDamage(int damage, Collider other){
        //if(damageOverTime){
        //    DoDamageOverTime(damageOverTimeDuration);
        //}
        if(other.gameObject.GetComponent<HealthBar>()){
            other.gameObject.GetComponent<HealthBar>().TakeDamage(damage);
        }
        //Debug.Log("destroy bullet");
        Destroy(gameObject,0.1f);
                    
    }

    void Detonation(){
        if(gameObject.transform.parent != null){
            gameObject.transform.parent.GetComponent<shooting>().Unload();
        }
        
        Instantiate(bigExplosionPrefab,bullet.transform.position, bullet.transform.rotation);
        Vector3 explosionPosition = bullet.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition,radius);
        //Debug.Log("Detonation: colliders"+ colliders);

        foreach(Collider hit in colliders){
            //Debug.Log("Detonation: foreach");
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Collider cl = hit.GetComponent<Collider>();
            if(rb!= null){
                rb.AddExplosionForce(explosionPower,explosionPosition,radius,0,ForceMode.Impulse);
            }
            if(cl!= null){
                //Debug.Log("Detonation: Damage cl");
                DoDamage(dmg,cl);
            }
            
        }

        Destroy(gameObject,0.1f);
    }
    //void DoDamageOverTime(float duration){
        //float durationCounter = 0;
       // while(durationCounter<duration){
            //InvokeRepeating("DoDamage",0.5f,damageInterval);
            //durationCounter++;
        //}
        
    //}
}
