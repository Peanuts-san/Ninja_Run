using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_con : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    public float jumpForce = 680.0f;
    public float jumpSpeed = 5;
    public int jumpLimit = 2;

    public int HP = 3;
    public int jumpCount = 0;
    public int secondJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ƒWƒƒƒ“ƒv
        float vy = this.rigid2D.velocity.y;
        if (vy == 0)
        {
            this.animator.SetBool("isJump", false);
            secondJump = 0;
        }
        else
        {
            this.animator.SetBool("isJump", true);
        }
        if (vy >= -jumpSpeed && vy <= jumpSpeed && secondJump < jumpLimit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
                jumpCount++;
                secondJump++;
            }
        }
    }
}
