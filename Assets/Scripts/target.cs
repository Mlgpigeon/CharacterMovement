using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Material onMaterial;
    public GameObject platform;
    // Script for the targets that activate platforms
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Throwable")
        {
            this.GetComponent<MeshRenderer>().material = onMaterial;
            platform.GetComponent<Animator>().SetBool("activated",true);
        }
    }
}
