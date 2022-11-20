using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_con : MonoBehaviour
{
    Manager_con manager;

    new AudioSource audio;

    public AudioClip clip_1, clip_2;

    public bool yetPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        this.manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
        this.audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (this.manager.getEnd())
        {
            dead_SE();
        }
        else if (Manager_con.getGoal())
        {
            goal_SE();
        }
    }

    public void dead_SE()
    {
        if (this.yetPlay)
        {
            this.audio.PlayOneShot(this.clip_1);
            this.yetPlay = false;
        }
    }

    public void goal_SE()
    {
        if (this.yetPlay)
        {
            this.audio.PlayOneShot(this.clip_2);
            this.yetPlay = false;
        }
    }
}
