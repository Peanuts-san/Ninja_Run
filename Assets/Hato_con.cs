using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hato_con : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.animator.SetTrigger("isHit");
        }
    }
}
