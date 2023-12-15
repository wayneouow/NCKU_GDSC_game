using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHp : MonoBehaviour
{
    [SerializeField] EnemySystem es;

    void Update()
    {
        GetComponent<Slider>().maxValue = es.MaxHP;
        GetComponent<Slider>().value = es.HP;
        if (!es.alive)
        {
            gameObject.SetActive(false);
        }
    }
}
