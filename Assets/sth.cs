using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sth : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cube)
        {
            Instantiate(cube, new Vector3(4, 1, 50), Quaternion.identity);
        }
        
    }
}
