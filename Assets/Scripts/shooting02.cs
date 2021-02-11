using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting02 : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public bool shootCD = true;
    public Image ShootSign;
    protected float Timer;
    public int shootTimer = 4;
 

    // Start is called before the first frame update
    void Start()
    {
       ShootSign.GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if(!shootCD){
            Timer += Time.deltaTime;
            if(Timer >= shootTimer){
                Timer = 0;
                shootCD = true;
                ShootSign.GetComponent<Image>().color = Color.green;
            }
        }
        
        if(Input.GetButtonDown("ShootKey02")){
            
            if(shootCD){
                shootCD = false;
                ShootSign.GetComponent<Image>().color = Color.red;
                GameObject TemporaryBullethandler;
                TemporaryBullethandler = Instantiate(bullet,bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

                TemporaryBullethandler.transform.Rotate(Vector3.left * 90);

                Rigidbody TempRigidbody;
                TempRigidbody = TemporaryBullethandler.GetComponent<Rigidbody>();
                TempRigidbody.AddForce(transform.forward * bulletSpeed);

                Destroy(TemporaryBullethandler, 7.0f);
                
            }
            else{

                print("shooting is on cooldown");
            }
        }
    }
   
}
