using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public PlayerMovement pm;
    public AudioClip movingSound;
    public AudioClip attackingSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.state == PlayerMovement.MovementState.walking || pm.state == PlayerMovement.MovementState.sprinting)
        {

            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }

            

    }
}
