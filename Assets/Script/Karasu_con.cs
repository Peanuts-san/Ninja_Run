using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karasu_con : MonoBehaviour
{
    public Transform target;
    public float speed = 0.5f;
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
        posB = this.transform.position; //自身の位置
        posN = target.position; //忍者の位置
        float distance = Vector3.Distance(posB, posN); //忍者との距離

        if (distance < 5.0f) rb.AddForce(force * speed);
    }
}