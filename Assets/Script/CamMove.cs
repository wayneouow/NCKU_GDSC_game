using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform ObjectPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = ObjectPos.position;
    }
}
