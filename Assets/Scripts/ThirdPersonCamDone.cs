using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamDone : MonoBehaviour
{
    [Header("References")]
    public Transform orientation1;
    public Transform player1;
    public Transform playerObj1;
    public Rigidbody rb1;

    public float rotationSpeed1;

    public Transform combatLookAt1;

    public GameObject thirdPersonCam1;
    public GameObject combatCam1;
    public GameObject topDownCam1;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        // switch styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);

        // rotate orientation
        Vector3 viewDir = player1.position - new Vector3(transform.position.x, player1.position.y, transform.position.z);
        orientation1.forward = viewDir.normalized;

        // rotate player obj
        if(currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation1.forward * verticalInput + orientation1.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj1.forward = Vector3.Slerp(playerObj1.forward, inputDir.normalized, Time.deltaTime * rotationSpeed1);
        }
        
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt1.position - new Vector3(transform.position.x, combatLookAt1.position.y, transform.position.z);
            orientation1.forward = dirToCombatLookAt.normalized;

            playerObj1.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam1.SetActive(false);
        thirdPersonCam1.SetActive(false);
        topDownCam1.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam1.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam1.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam1.SetActive(true);

        currentStyle = newStyle;
    }
}
