using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] PlayerValue val;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Slider>().value = val.HP;
    }
}
