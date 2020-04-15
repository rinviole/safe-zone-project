using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Transform target;
    Vector2 startDir;
    float slingTime = 2.5f; //Adds a force for 2.5 secs then deactivate
    bool stopForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        stopForce = false;
        target = GameObject.FindGameObjectWithTag("Safe Zone").transform;
        if (target != null)
        {
            startDir = target.transform.position - transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > slingTime && !stopForce)
        {
            rb.AddForce(startDir * speed * Time.deltaTime, ForceMode2D.Impulse);
            stopForce = true;
        }
       
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 0.1f);
    }
}
