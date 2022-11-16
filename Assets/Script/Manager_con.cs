using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_con : MonoBehaviour
{
    Ninja_con ninja;
    public GameObject CD = null;
    Text TEXT;

    float countDown = 4.0f;
    float pre_CountDown = 2.0f;

    int counter;
    int timer;

    float now = 1.0f;

    public bool play = false;

    bool ninja_dead;

    // Start is called before the first frame update
    void Start()
    {
        this.ninja = (GameObject.Find("Ninja")).GetComponent<Ninja_con>();
        this.TEXT = this.CD.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        this.ninja_dead = this.ninja.getDead();
        if (this.pre_CountDown >= 0)
        {
            this.pre_CountDown -= Time.deltaTime;
        }
        else
        {
            if (this.countDown > 0)
            {
                this.countDown -= Time.deltaTime;
                this.counter = (int)this.countDown;
                if (this.counter > 0)
                {
                    Debug.Log(counter);
                    this.TEXT.text = counter.ToString("D");
                }
                else
                {
                    Debug.Log("Start!");
                    this.play = true;
                    this.TEXT.text = "Start!";
                }
            }
            else
            {
                if (this.play)
                {
                    this.now += Time.deltaTime;
                    this.timer = (int)this.now;
                    Debug.Log(this.timer);

                    this.TEXT.text = null;
                }
                else if (this.ninja_dead)
                {
                    Debug.Log("You Died!");
                    this.TEXT.text = "You Died!";
                }
            }
        }
       
    }

    public bool getPlay()
    {
        return this.play;
    }

    public void setPlay(bool b)
    {
        this.play = b;
    }
}
