using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MultiArea : MonoBehaviour
{
    public EnemySystem es;
    public void WhenCheckTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            es.found = true;
            Debug.Log("Check Sphere Enter1");
        }
        
    }
    public void WhenCheckTriggerExit(Collider other)
    {
        Debug.Log("Check Sphere Exit1");
        es.found = false;
    }
    public void WhenAttackTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Attack Sphere Enter1");
            es.near = true;
        }
    }
    public void WhenAttackTriggerExit(Collider other)
    {
        Debug.Log("Attack Sphere Exit1");
        es.near = false;
    }

}
