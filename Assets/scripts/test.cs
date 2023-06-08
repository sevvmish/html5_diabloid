using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            print(other.gameObject.name);
        }
    }


}
