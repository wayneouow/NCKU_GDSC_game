using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public Transform player;
    public float eatRange;
    public PlayerValue pv;
    public int value;
    public GameObject pickParticle;
    [SerializeField] bool inRange;
    // Start is called before the first frame update
    void Start()
    {
    }
    private float a = 0f;
    // Update is called once per frame
    void Update()
    {

        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= eatRange)
            inRange = true;
        else
            inRange = false;

        if (distanceToPlayer.magnitude <= eatRange && Input.GetKeyDown(KeyCode.F)) PickUp();
    }
    private void PickUp()
    {
        pv.HP += value;
        inRange = false;
        Instantiate(pickParticle, pv.transform);
        Destroy(gameObject);
    }
}
