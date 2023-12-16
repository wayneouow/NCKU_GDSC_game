using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValue : MonoBehaviour
{
    public int money = 0;
    public int HP = 100;
    public int MaxHP = 100;
    public bool alive = true;
    public GameObject gameOver;
    public GameObject bgm;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP > MaxHP)
            HP = MaxHP;
        if (HP <= 0)
        {
            HP = 0;
            alive= false;
        }
        if (!alive)
        {
            bgm.GetComponent<AudioSource>().Pause();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOver.gameObject.SetActive(true);
        }
    }
}
