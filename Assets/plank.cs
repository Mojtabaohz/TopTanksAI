using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plank : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 200f;
    int damage = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }

    void move(){
        if(Input.GetKey(KeyCode.Space)){
            transform.Rotate(Vector3.up*Time.deltaTime *speed);
        }
    }
    void OnTriggerEnter(Collider other){
        //other.gameObject.GetComponent<HealthBar>().TakeDamage(damage);
    }



}


