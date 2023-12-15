using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public Transform player;
    public float pickUpRange;
    public PlayerValue pv;
    public Material material;
    public int value;
    public GameObject pickParticle;
    [SerializeField] bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = material;
    }
    private float a = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, a++, 90);

        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange)
            inRange = true;
        else
            inRange = false;

        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F)) PickUp();
    }
    private void PickUp()
    {
        pv.money += value;
        inRange = false;
        Instantiate(pickParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
