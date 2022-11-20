using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hato_con : MonoBehaviour
{
    Animator animator;
    Ninja_con ninja;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.ninja = (GameObject.Find("Ninja")).GetComponent<Ninja_con>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !this.ninja.getIsDamage())
        {
            this.animator.SetTrigger("isHit");
        }
    }
}
