using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_con : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3 posB;
    Vector3 posN;
    Rigidbody2D rb;
    Vector3 force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        force = new Vector3(-10.0f, 0.0f, 0.0f);
        target = GameObject.Find("Ninja").transform;
    }

    // Update is called once per frame
    void Update()
    {
        posB = this.transform.position; //é©êgÇÃà íu
        posN = target.position; //îEé“ÇÃà íu
        float distance = Vector3.Distance(posB, posN); //îEé“Ç∆ÇÃãóó£

        if (distance < 5.0f)rb.AddForce(force);
    }
}
