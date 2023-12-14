using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotaionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        viewDir = new Vector3(viewDir.x, 0f, viewDir.z);
        orientation.forward = viewDir;//.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        inputDir = new Vector3(inputDir.x, 0f, inputDir.z);
        if (inputDir != Vector3.zero)
        {
        
            //player.forward = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotaionSpeed);
            Vector3 R = Vector3.Slerp(player.forward, inputDir.normalized, Time.deltaTime * rotaionSpeed);
            player.transform.forward = new Vector3(R.x, 0f, R.z);
        }

    }
}
