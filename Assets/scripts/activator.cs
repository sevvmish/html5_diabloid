using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.layer.Equals(7))
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.layer.Equals(7))
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
