using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_con : MonoBehaviour
{
    Ninja_con ninja;
    bool dead;
    Manager_con manager;
    bool play;

    public float speed = 1;
    public float maxSpeed = 1.0f;
    public float par = 1.01f;

    public float goal = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.ninja = (GameObject.Find("Ninja")).GetComponent<Ninja_con>();
        this.manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
    }

    // Update is called once per frame
    void Update()
    {
        this.play = this.manager.getPlay();
        this.dead = ninja.getDead();
        if (this.manager.play)
        {
            if (this.dead)
            {
                this.speed *= 0;
                this.manager.setPlay(false);
            }
            else if (getX() < this.goal)
            {
                if (speed < maxSpeed)
                {
                    speed *= par;
                }
            }
            else
            {
                speed /= par;
                if (speed < 0.01f)
                {
                    speed = 0;
                    this.manager.setPlay(false);
                }
            }
            transform.Translate(this.speed, 0, 0);
        }
    }

    public float getX()
    {
        return transform.position.x;
    }
}
