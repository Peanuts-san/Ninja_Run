using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_con : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    Camera_con camera;
    Manager_con manager;

    public float jumpForce = 680.0f;
    public float jumpSpeed = 5;
    public int jumpLimit = 2;
    public float death_Jump = 10.0f;

    public int HP = 3;
    public int jumpCount = 0;
    public int secondJump = 0;

    public bool canPlay = true;

    float death_X;
    public bool dead = false;
    public bool isDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.camera = (GameObject.Find("Main Camera")).GetComponent<Camera_con>();
        this.manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
    }

    // Update is called once per frame
    void Update()
    {
        float sp = this.camera.speed;
        // 走る
        this.animator.SetFloat("Run_Speed", sp);

        // ジャンプ
        float vy = this.rigid2D.velocity.y;
        if (vy == 0.0f)
        {
            this.animator.SetBool("isJump", false);
            secondJump = 0;
        }
        else
        {
            this.animator.SetBool("isJump", true);
        }

        if (this.canPlay)
        {
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
        else
        {
            float death_Time = 0.0f;
            death_Time++;
            // ゾンビ
            if (death_Time >= 120.0f)
            {
                this.dead = true;
            }
        }

        // ダメージ
        if (isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time));
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, level);
        }

        // 衝突死
        float camera_X = camera.getX();
        float dis = 0;
        if (this.death_X > 0)
        {
            dis = death_X - camera_X;
        }
        if (dis <= -10)
        {
            setDeath();
        }

        // 落下死
        if (transform.position.y < -10.0f)
        {
            setDeath();
            this.animator.SetBool("isHit", true);
        }

        // 鳥死
        if (HP <= 0)
        {
            this.animator.SetBool("isHit", true);
        }

        // 死亡
        if (this.dead)
        {
            this.manager.setPlay(false);
        }

        // アニメーションスピード
        if (sp < 0.05f)
        {
            this.animator.speed = sp * 20.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
    }

    // 壁にぶつかる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            this.death_X = transform.position.x;
            Debug.Log("wall");
            this.animator.SetBool("isHit", true);
            this.canPlay = false;
        }
    }

    // 鳥にぶつかる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bird")
        {
            Debug.Log("bird");
            this.animator.SetBool("isHit", true);
            if (this.HP > 0)
            {
                StartCoroutine("Damage");
                this.animator.SetBool("isHit", false);
            }
            else
            {
                setDeath();
            }
        }
    }

    IEnumerator Damage()
    {
        this.HP--;
        Debug.Log(this.HP);
        isDamage = true;
        yield return new WaitForSeconds(2.0f);
        this.animator.SetBool("isHit", false);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        isDamage = false;
    }

    public bool getDead()
    {
        return this.dead;
    }

    private void setDeath()
    {
        this.dead = true;
        this.canPlay = false;
    }
}
