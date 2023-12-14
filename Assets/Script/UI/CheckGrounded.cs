using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGrounded : MonoBehaviour
{
    bool grounded;
    [SerializeField] private GameObject gameObject;
    public LayerMask Ground;
    void Update()
    {
        bool grounded = Physics.Raycast(gameObject.transform.position, Vector3.down, 2 * 0.5f + 0.2f, Ground);
        GetComponent<Text>().text = "grounded: " + grounded;
    }
}
