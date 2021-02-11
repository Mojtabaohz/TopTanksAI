using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int dmg = 0 ;
    public int bulletSpeed = 100;
    public float reloadSpeed = 2f;
    public int ammoCount = 1; 
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start(){
        SetBulletAttribute(bullet);
    }
    private void OnTriggerEnter(Collider obj){
        if(obj.gameObject.tag.Equals("Player")){
            //Debug.Log("Object report "+ obj.gameObject + " . " + obj.gameObject.transform.GetChild(1));
            if(obj.gameObject.GetComponent<shooting>().loaded){
                obj.gameObject.GetComponent<shooting>().DestroyCurrentBullet(obj.gameObject.transform.GetChild((obj.gameObject.transform.childCount-1)).gameObject);
                obj.gameObject.GetComponent<shooting>().Unload();
            }
            
            obj.GetComponent<shooting>().ammoCount = ammoCount;
            obj.GetComponent<shooting>().bulletSpeed = bulletSpeed;
            obj.GetComponent<shooting>().bullet = bullet;
            obj.GetComponent<shooting>().dmg = dmg;
            obj.GetComponent<shooting>().reloadSpeed = reloadSpeed;
            //obj.GetComponent<Player01Controller>().SpeedBuff(4);
            
            // PickUpBox will decrease the spawned box in result manager so there will be limited amount of box at every stage of the game.
            FindObjectOfType<ResultManager>().PickUpBox();

            obj.GetComponent<shooting>().BulletInstantiate(obj);
            //GameObject TemporaryBullet;
            //TemporaryBullet = Instantiate(bullet, obj.GetComponent<shooting>().bulletEmitter.transform.position, obj.GetComponent<shooting>().bulletEmitter.transform.rotation) as GameObject;
            //TemporaryBullet.GetComponent<Bullet>().collisionEnable = false;
            //TemporaryBullet.transform.Rotate(Vector3.left * 90);
           // TemporaryBullet.transform.parent = obj.GetComponent<shooting>().transform;
           // TemporaryBullet.GetComponent<Rigidbody>().useGravity = false;
           // TemporaryBullet.GetComponent<Rigidbody>().detectCollisions = false;
            Destroy(gameObject);
        }
        else if(obj.gameObject.tag.Equals("bullet")){
            FindObjectOfType<ResultManager>().PickUpBox();
            Destroy(gameObject);
            
        }

    }

    void SetBulletAttribute(GameObject obj){
        obj.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        obj.GetComponent<Bullet>().dmg = dmg;
        //bullet.GetComponent<Bullet>().aoe = aoe;
    }


}
