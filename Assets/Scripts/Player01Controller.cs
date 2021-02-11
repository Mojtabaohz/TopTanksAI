using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Controller : MonoBehaviour
{
    [SerializeField]
    //float moveSpeed ;
    Vector3 forward, right;
    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right =Quaternion.Euler(new Vector3(0,90,0)) * forward;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            Move();
            if(Input.GetButtonDown("ShootKey")){
            gameObject.GetComponent<shooting>().Shoot();
            }
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"),0,Input.GetAxis("VerticalKey"));
        Vector3 rightMovement = right * (gameObject.GetComponent<shooting>().moveSpeed) * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * (gameObject.GetComponent<shooting>().moveSpeed) * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    
}
