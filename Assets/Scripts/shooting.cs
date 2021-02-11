using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletEmitter;
    public GameObject bullet;
    public GameObject playerBase;
    public int dmg;
    public float bulletSpeed = 100f;
    public bool loaded = true;
    public int ammoCount = 0 ;
    public float reloadSpeed = 4;
    protected float Timer;
    protected float buffTimer;
    public bool Buff;
    public float buffDuration;
    public GameObject shootSign;
    public AudioSource ShootSound;
    public AudioSource ReloadSound;
    public AudioSource UziSound;
    public AudioSource SniperSound;
    public float moveSpeed;
    public float MS = 4f;
    // Start is called before the first frame update
    void Start()
    {
       loaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Reload(loaded); 
        BuffCheck(Buff);
    }
    public void Shoot(){
        
        Shooting();
        //Debug.Log("shoot called");
    }

    void Shooting(){
        if(loaded){
            Unload();
            GameObject TemporaryBullethandler;
            TemporaryBullethandler = gameObject.transform.GetChild((gameObject.transform.childCount-1)).gameObject;
            TemporaryBullethandler.transform.parent = null;
            TemporaryBullethandler.GetComponent<Rigidbody>().useGravity = true;
            TemporaryBullethandler.GetComponent<Rigidbody>().detectCollisions = true;
            //Instantiate(bullet,bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

            TemporaryBullethandler.transform.Rotate(Vector3.left * 90);

            Rigidbody TempRigidbody;
            TempRigidbody = TemporaryBullethandler.GetComponent<Rigidbody>();
            TempRigidbody.AddForce(transform.forward * (bulletSpeed + moveSpeed));
            if(bullet.name == "bullet" || bullet.name == "Bomb"){
                TempRigidbody.AddForce(transform.up *  (250));
            }
            
            TemporaryBullethandler.GetComponent<Bullet>().collisionEnable = true;
            Destroy(TemporaryBullethandler, 6.0f);
                if (bulletSpeed<=1500){
                    ShootSound.Play(); 
                }
                else if (bulletSpeed <= 2000) {
                    UziSound.Play();
                } else {
                    SniperSound.Play();
                }                
            }
            else{

                //print("shooting is on cooldown");
                //ReloadSound.Play();
            }
    }

    void Reload(bool _loaded){
        if(!_loaded){
            Timer += Time.deltaTime;
            if(Timer >= reloadSpeed){
                Timer = 0; 
                if(ammoCount < 0){
                    BulletInstantiate(gameObject.GetComponent<Collider>());
                }
                else if(ammoCount>0){
                    BulletInstantiate(gameObject.GetComponent<Collider>());
                    ammoCount -= 1;
                }
                else if(ammoCount == 0 ){
                    SetDefaultWeapon(this.gameObject);
                    BulletInstantiate(gameObject.GetComponent<Collider>());
                    ammoCount -= 1;
                }
                loaded = true;
                shootSign.SetActive(true);
            }
        }
        else{
            return;
        }
    }

    void BuffCheck(bool _buff){
        if(_buff){
        buffTimer += Time.deltaTime;
            if(buffTimer >= buffDuration){
                buffTimer = 0;
                moveSpeed = MS;
                Buff = false;
            }
        }
    }

    public void SetDefaultWeapon(GameObject player){
        player.GetComponent<shooting>().ammoCount = FindObjectOfType<ResultManager>().defaultWeapon.GetComponent<AmmoBox>().ammoCount;
        player.GetComponent<shooting>().bulletSpeed = FindObjectOfType<ResultManager>().defaultWeapon.GetComponent<AmmoBox>().bulletSpeed;
        player.GetComponent<shooting>().bullet = FindObjectOfType<ResultManager>().defaultWeapon.GetComponent<AmmoBox>().bullet;
        player.GetComponent<shooting>().dmg = FindObjectOfType<ResultManager>().defaultWeapon.GetComponent<AmmoBox>().dmg;
        player.GetComponent<shooting>().reloadSpeed = FindObjectOfType<ResultManager>().defaultWeapon.GetComponent<AmmoBox>().reloadSpeed;      
    }

    public void BulletInstantiate(Collider obj){
        if(!loaded){
            Timer = 0 ;
            if(gameObject.transform.childCount < 4){
                GameObject TemporaryBullet;
                TemporaryBullet = Instantiate(bullet, obj.GetComponent<shooting>().bulletEmitter.transform.position, obj.GetComponent<shooting>().bulletEmitter.transform.rotation) as GameObject;
                TemporaryBullet.GetComponent<Bullet>().collisionEnable = false;
                TemporaryBullet.transform.Rotate(Vector3.left * 90);
                TemporaryBullet.transform.parent = obj.GetComponent<shooting>().transform;
                TemporaryBullet.GetComponent<Rigidbody>().useGravity = false;
                TemporaryBullet.GetComponent<Rigidbody>().detectCollisions = false;
            }
        }
    }

    public void DestroyCurrentBullet(GameObject currentBullet){
        Destroy(currentBullet);
    }

    public void Unload(){
        loaded = false;
        shootSign.SetActive(false);

    }
   
}
