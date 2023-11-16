using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] private Rigidbody playerRB;
    // Update is called once per frame
    void Update()
    {
        Vector3 v3Velocity = playerRB.velocity; 
        GetComponent<Text>().text = v3Velocity.magnitude.ToString() + "\n" + v3Velocity.ToString();
    }
}
