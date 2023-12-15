using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVal : MonoBehaviour
{
    [SerializeField] PlayerValue val;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = val.money.ToString();
    }
}
