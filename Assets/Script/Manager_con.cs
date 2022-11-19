using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_con : MonoBehaviour
{
    Ninja_con ninja;
    public GameObject CD = null;
    Text TEXT;
    public GameObject dis_button = null;
    Text tx;

    new AudioSource audio;

    float countDown = 4.0f;
    float pre_CountDown = 2.0f;

    int counter;
    int timer;

    float now = 1.0f;

    public bool play = false;
    public bool end = false;

    bool ninja_dead;

    public static bool isGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        this.ninja = (GameObject.Find("Ninja")).GetComponent<Ninja_con>();
        this.audio = GetComponent<AudioSource>();
        this.TEXT = this.CD.GetComponent<Text>();
        this.tx = this.dis_button.GetComponent<Text>();
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
                    play_BGM();
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
                    StartCoroutine("you_Die");
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

    public bool getEnd()
    {
        return this.end;
    }

    public static bool getGoal()
    {
        return isGoal;
    }

    public static void setGpal(bool b)
    {
        isGoal = b;
    }

    void play_BGM()
    {
        this.audio.loop = true;
        this.audio.Play();
    }

    void stop_BGM()
    {
        this.audio.loop = false;
        this.audio.Stop();
    }

    IEnumerator you_Die()
    {
        stop_BGM();
        this.end = true;
        Debug.Log("You Died!");
        this.TEXT.text = "Game Over!";
        this.tx.text = "Press B";
        yield return new WaitForSeconds(1.0f);
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
