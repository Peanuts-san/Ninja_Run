using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Teki : MonoBehaviour
{
    public GameObject player;
    public float attackPower = 5.0f;
    public float posi = 7.5f;
    Rigidbody2D rigid2D;
    GameObject camera;
    public bool isAttack = false;
    float startPos;
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = this.transform.position.x;
        this.player = GameObject.Find("Ninja");
        rigid2D = GetComponent<Rigidbody2D>();
        this.camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pPos = player.transform.position;
        Vector2 myPos = this.transform.position;
        Vector3 caPos = this.camera.transform.position;
        float distance_X = myPos.x - caPos.x;
        float dis = Vector2.Distance(myPos, pPos);


        if (this.isAttack)
        {
            this.rigid2D.constraints = RigidbodyConstraints2D.None;
            this.rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.rigid2D.AddForce(transform.up * dis * -attackPower);
        }
        else
        {
            this.rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            this.rigid2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            float sd = myPos.x - startPos;

            if (distance_X <= posi && sd <= 100)
            {
                transform.position = new Vector3((caPos.x + posi), transform.position.y, transform.position.z);
            }
            else if (distance_X > 0 && sd > 100)
            {
                this.isAttack = true;
            }
        }
    }
}
