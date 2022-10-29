using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Ninja : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    GameObject camera;
    Control_Camera cc;

    public float jumpForce = 100.0f;
    public float jumpSpeed = 5;
    public float walkForce = 30.0f;
    public float maxWalkSpeed = 2.0f;
    public int HP = 3;

    bool isBard = false;
    bool canJump = true;
    bool isHit = false;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.camera = GameObject.Find("Main Camera");
        this.cc = camera.GetComponent<Control_Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Run
        Vector3 cameraPos = this.camera.transform.position;
        float speedx = cc.speed;
        if (!isHit || HP < 1)
        {
            transform.position = new Vector3(cameraPos.x, transform.position.y, transform.position.z);
            this.rigid2D.AddForce(transform.right * speedx * this.walkForce);
            this.animator.SetFloat("WalkFloat", speedx);
        }

        // Jump
        float vy = this.rigid2D.velocity.y;
        if (vy == 0)
        {
            this.animator.SetBool("JumpBool", false);
        }
        else
        {
            this.animator.SetBool("JumpBool", true);
        }
        if (vy > -jumpSpeed && vy < jumpSpeed)
        {
            if (canJump & Input.GetKeyDown(KeyCode.Space))
            {
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }
        }

        // AnimationSpeed
        if (this.rigid2D.velocity.y <= 0.05f)
        {
            this.animator.speed = speedx * 7.5f / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        if (isHit)
        {
            this.animator.SetBool("HitBool", true);
        }

        // Dead
        if(transform.position.x < (cameraPos.x - 10) || transform.position.x > (cameraPos.x + 10) || transform.position.y < (cameraPos.y - 6))
        {
            this.isDead = true;
            Debug.Log("Dead!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kabe")
        {
            Debug.Log("Kabe");
            this.isHit = true;
            this.canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
            this.HP--;
            this.isBard = true;
        }
    }
}
