using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CureLogic : MonoBehaviourPun
{ 
    float t = 0;
    
    private void OnTriggerEnter(Collider other)
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
      
        t = 0;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t >= 30)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            t= 0;
        }
    }
}
