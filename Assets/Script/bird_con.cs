using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_con : MonoBehaviour
{
    private GameObject target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        target = GameObject.Find("Ninja");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<Ninja_con>().isArea == true)
        {
            Debug.Log("circle isArea");
            GetComponent<Renderer>().material.color = Color.red;
            transform.LookAt(target.transform);
            transform.position += transform.forward * speed;
        }

        else
        {
            //Debug.Log("circle !isArea");
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
