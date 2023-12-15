using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gameplay : MonoBehaviour
{
    public GameObject manual;
    public void showGameplay()
    { 
        manual.SetActive(true);
    }
    public void closeGameplay()
    {
        manual.SetActive(false);
    }
}
