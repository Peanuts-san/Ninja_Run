using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result_con : MonoBehaviour
{
    Text TX_1;
    public GameObject RESULT = null;
    Text TX_2;
    public GameObject HP = null;
    Text TX_3;
    public GameObject JUMP = null;
    Text TX_4;
    public GameObject SCORE = null;
    Text TX_5;
    public GameObject NEW = null;
    Text TX_6;
    public GameObject PT = null;

    public bool isClear;
    public int nhp;
    public int njp;

    public int score = 0;
    public float p = 0;

    public static int best = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.TX_1 = this.RESULT.GetComponent<Text>();
        this.TX_2 = this.HP.GetComponent<Text>();
        this.TX_3 = this.JUMP.GetComponent<Text>();
        this.TX_4 = this.SCORE.GetComponent<Text>();
        this.TX_5 = this.NEW.GetComponent<Text>();
        this.TX_6 = this.PT.GetComponent<Text>();
        this.isClear = Manager_con.getGoal();
        this.nhp = Ninja_con.getHP();
        this.njp = Ninja_con.getJump();

        if (this.isClear)
        {
            this.TX_1.text = "Game Clear!";
            this.score += 1000;
            p = 10000;
        }
        else
        {
            this.TX_1.text = "Game Over!";
            p = 1000;
        }

        this.TX_2.text = "HP:" + this.nhp;
        this.TX_3.text = "Jump Count:" + this.njp;

        this.score += this.nhp * 100;
        if (this.njp > 0)
        {
            this.score += (int)p / this.njp;
        }

        this.TX_4.text = "Score:" + this.score;
        if (this.score > best)
        {
            best = this.score;
            this.TX_5.text = "New!";
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            this.TX_6.text = "Play:B        ";
            SceneManager.LoadScene("GameScene");
        }
        else if (Input.GetButtonDown("Submit"))
        {
            this.TX_6.text = "       Title:A";
            SceneManager.LoadScene("TitleScene");
        }
    }
}
