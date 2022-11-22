using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    private GameObject player;
    private Rigidbody enemyRb;

    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyStartMethod();
    }

    // Update is called once per frame
    void Update()
    {
        enemyUpdateMethod();
    }

    public void enemyStartMethod()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    public void enemyUpdateMethod()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (enemyRb.transform.position.y < -10) 
        {
            Destroy(gameObject);
        }
    }
}
