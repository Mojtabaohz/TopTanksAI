using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    public int currentHealth;
    public bool alive = true;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public AudioSource DestroySound;

    public void TakeDamage(int damage){
        if(!alive){
            return;
        }
        currentHealth -= damage;
        SetHealth(currentHealth);
        
        if(currentHealth <= 0){
            DestroySound.Play();
            currentHealth = 0;
            alive = false;
            gameObject.SetActive(false);
            
        }
        
    }
    public void Heal(int heal){
        if(currentHealth+heal >= maxHealth){
            currentHealth = maxHealth;
            SetHealth(currentHealth);
            //Debug.Log("Heal function call in if");
        }
        else{
            currentHealth += heal;
            SetHealth(currentHealth);
            //Debug.Log("Heal function call in else");
        }
        
    }

    public void SetHealth(int health){
        slider.value = health; 
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }




    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        SetHealth(currentHealth);
        fill.color = gradient.Evaluate(1f);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
