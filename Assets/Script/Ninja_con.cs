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

    public int HP = 3;
    public int jumpCount = 0;
    public int secondJump = 0;

    public bool canPlay = true;

    float death_X;
    public bool dead = false;
    public bool isDamage = false;
    public bool isArea;


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
        // ����
        this.animator.SetFloat("Run_Speed", sp);

        // �W�����v
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
            death_Time += Time.deltaTime;
            // �]���r
            if (death_Time >= 2.0f)
            {
                Debug.Log("Zombi");
                this.dead = true;
            }
        }

        // �_���[�W
        if (isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time));
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, level);
        }

        // �Փˎ�
        float camera_X = camera.getX();
        this.death_X = this.transform.position.x;
        float dis = 0;
        if (this.death_X > 0)
        {
            dis = death_X - camera_X;
            //Debug.Log(dis);
        }
        if (dis <= -10)
        {
            Debug.Log("shototu desu");
            setDeath();
        }

        // ������
        if (transform.position.y < -7.5f)
        {
            Debug.Log("rakka desu");
            setDeath();
            this.animator.SetBool("isHit", true);
        }

        // ����
        if (HP <= 0)
        {
            this.animator.SetBool("isHit", true);
        }

        // ���S
        if (this.dead)
        {
            this.manager.setPlay(false);
        }

        // �A�j���[�V�����X�s�[�h
        if (sp < 0.05f)
        {
            this.animator.speed = sp * 20.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
    }

    // �ǂɂԂ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("wall");
            this.animator.SetBool("isHit", true);
            this.canPlay = false;
        }
    }

    // ���ɂԂ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bird" && !this.isDamage)
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
                Debug.Log("tori desu");
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DangerArea") isArea = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DangerArea") isArea = false;
    }
}
