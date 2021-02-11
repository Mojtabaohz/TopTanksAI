using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneKiller : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals("Player")){
           // other.GetComponent<HealthBar>().TakeDamage(damage);
        }
            
    }
}
