using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_con : MonoBehaviour
{
    Ninja_con ninja;
    bool dead;
    Manager_con manager;
    bool play;

    bool yet_Start = true;

    public float speed = 1;
    public float maxSpeed = 1.0f;
    public float par = 1.01f;

    public float goal = 50.0f;

    public float timer;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        this.ninja = (GameObject.Find("Ninja")).GetComponent<Ninja_con>();
        this.manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
        this.audio = GetComponent<AudioSource>();
        this.timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.play = this.manager.getPlay();
        this.dead = ninja.getDead();
        if (this.play)
        {
            if (this.yet_Start)
            {
                play_BGM();
                this.yet_Start = false;
            }
            this.timer += Time.deltaTime;
            StartCoroutine("VolumeUp");
            if (this.dead)
            {
                stop_BGM();
                this.speed *= 0;
                this.manager.setPlay(false);
            }
            else if (getX() < this.goal)
            {
                if (speed < maxSpeed)
                {
                    if (this.timer >= 1.0f)
                    {
                        speed *= par;
                        this.timer = 0.0f;
                    }
                }
            }
            else
            {
                speed /= par;
                StartCoroutine("VolumeDown");
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

    void play_BGM()
    {
        this.audio.Play();
    }

    void stop_BGM()
    {
        this.audio.Stop();
    }

    IEnumerator VolumeUp()
    {
        while (audio.volume < 0.4f)
        {
            audio.volume += 0.001f;
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator VolumeDown()
    {
        while (audio.volume > 0.0f)
        {
            audio.volume -= 0.001f;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
