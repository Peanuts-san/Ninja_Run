using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Camera : MonoBehaviour
{
    public float speed = 1;
    public float maxSpeed = 2;
    public float per = 1.01f;
    public string playerName;
    Control_Ninja player;
    Control_Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(playerName).GetComponent<Control_Ninja>();
        manager = GameObject.Find("manager").GetComponent<Control_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isStart)
        {
            if (player.isDead)
            {
                this.speed *= 0;
            }
            else
            {
                if (this.speed < this.maxSpeed)
                {
                    this.speed *= this.per;
                }
            }
            transform.Translate(this.speed, 0, 0);
        }
    }
}
