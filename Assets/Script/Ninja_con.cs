using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja_con : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    new Camera_con camera;
    Manager_con manager;
    Audio_con ad;

    new AudioSource audio;
    public AudioClip jump_Clip;
    public AudioClip hit_Clip;

    Text tx_1;
    public GameObject dis_HP = null;
    Text tx_2;
    public GameObject dis_Jump = null;

    public float jumpForce = 680.0f;
    public float jumpSpeed = 5;
    public int jumpLimit = 2;

    public static int HP = 3;
    public static int jumpCount = 0;
    public int secondJump = 0;

    public bool canPlay = true;

    float camera_X;
    float death_X;
    float dis;
    public bool dead = false;
    public bool isDamage = false;
    public bool isArea;

    public bool yet_Death = false;

    float death_Time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.camera = (GameObject.Find("Main Camera")).GetComponent<Camera_con>();
        this.manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
        this.ad = (GameObject.Find("AudioManager")).GetComponent<Audio_con>();
        this.audio = GetComponent<AudioSource>();
        this.tx_1 = this.dis_HP.GetComponent<Text>();
        this.tx_2 = this.dis_Jump.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.manager.getPlay())
        {
            this.tx_1.text = HP.ToString("D");
            this.tx_2.text = jumpCount.ToString("D");
        }

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
                if (Input.GetButtonDown("Jump"))
                {
                    jump_SE();
                    this.rigid2D.AddForce(transform.up * this.jumpForce);
                    jumpCount++;
                    secondJump++;
                }
            }
        }
        else
        {
            death_Time += Time.deltaTime;
            // ゾンビ
            if (death_Time >= 1.0f)
            {
                Debug.Log("Zombi");
                setDeath();
            }
        }

        // ダメージ
        if (isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time));
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, level);
        }

        // 衝突死
        this.camera_X = camera.getX();
        this.death_X = this.transform.position.x;
        this.dis = this.camera_X - this.death_X;
        Debug.Log("camera_X:" + this.camera_X + ", death_X:" + this.death_X + ", distance:" + this.dis);
        if (this.dis > 6.5f)
        {
            Debug.Log("shototu desu");
            setDeath();
        }

        // 落下死
        if (transform.position.y < -7.5f)
        {
            Debug.Log("rakka desu");
            setDeath();
            this.animator.SetBool("isHit", true);
        }

        // 鳥死
        if (HP <= 0)
        {
            this.animator.SetBool("isHit", true);
            setDeath();
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
            Debug.Log("wall");
            this.animator.SetBool("isHit", true);
            hit_SE();
            this.canPlay = false;
        }
    }

    // 鳥にぶつかる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bird" && !this.isDamage)
        {
            Debug.Log("bird");
            this.animator.SetBool("isHit", true);
            if (HP > 0)
            {
                hit_SE();
                StartCoroutine("Damage");
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
        HP--;
        Debug.Log(HP);
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
        {
            this.dead = true;
            this.canPlay = false;
            this.manager.setPlay(false);
            this.yet_Death = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DangerArea") isArea = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DangerArea") isArea = false;
    }

    void jump_SE()
    {
        this.audio.PlayOneShot(this.jump_Clip);
    }

    void hit_SE()
    {
        this.audio.PlayOneShot(this.hit_Clip);
    }

    public static int getJump()
    {
        return jumpCount;
    }

    public static int getHP()
    {
        return HP;
    }
}
