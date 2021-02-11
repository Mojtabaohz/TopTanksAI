using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksAttr : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Tank attributes")]
    [Tooltip("blue team is 1 and red team is 2")]
    public int team; // 1 is blue and 2 is red, 0 will be unassigned
    [Space(10)]
    [SerializeField]
    public float damage;
    
    [SerializeField]
    public float penetration;
    [SerializeField]
    public float fireRange;
    [SerializeField]
    public float viewRange;

    [SerializeField]
    public float reloadSpeed;
    
    [SerializeField]
    public float moveSpeed;
    [Space(10)]
    [SerializeField]
    public bool alive;
    [SerializeField]
    public float health;

    [SerializeField]
    public float armor;
    
    [SerializeField]
    public float currentHealth;
    
    [Space(10)]
    public bool spotted;
    
   
    


    void Start()
    {
        alive = true;
        spotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
