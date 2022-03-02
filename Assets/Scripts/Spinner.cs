using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    public float spinSpeed;
    

    void Start()
    {
       
    }

 
    void Update()
    {
        

        transform.Rotate(transform.up, spinSpeed * Time.deltaTime);
        //transform.Rotate(transform.right, randomSpin.x * Time.deltaTime);
        //transform.Rotate(transform.forward, randomSpin.z * Time.deltaTime);

        
    }
}
