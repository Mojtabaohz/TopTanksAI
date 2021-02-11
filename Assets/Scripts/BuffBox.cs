using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBox : MonoBehaviour
{
    [SerializeField]
    public int selfHealth;
    public int baseHealth;
    public int speedBuff;
    public float speedBuffDuration;

    public void OnTriggerEnter(Collider obj){
        if(obj.gameObject.tag.Equals("Player")){
            if(selfHealth!=0){
                obj.GetComponent<HealthBar>().Heal(selfHealth);
                
            }
            if(baseHealth!=0){
                obj.GetComponent<shooting>().playerBase.GetComponent<HealthBar>().Heal(baseHealth);
            }
            if(speedBuff!=0){
                obj.GetComponent<shooting>().moveSpeed += speedBuff;
                obj.GetComponent<shooting>().buffDuration = speedBuffDuration;
                obj.GetComponent<shooting>().Buff = true;
            }
            FindObjectOfType<ResultManager>().PickUpBox();
            Destroy(gameObject);
        }
        else if(obj.gameObject.tag.Equals("bullet")){
            FindObjectOfType<ResultManager>().PickUpBox();
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
