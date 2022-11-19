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

    public bool isClear;
    public int nhp;
    public int njp;

    public int score = 0;
    public float p = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.TX_1 = this.RESULT.GetComponent<Text>();
        this.TX_2 = this.HP.GetComponent<Text>();
        this.TX_3 = this.JUMP.GetComponent<Text>();
        this.TX_4 = this.SCORE.GetComponent<Text>();
        this.isClear = Manager_con.getGoal();
        this.nhp = Ninja_con.getHP();
        this.njp = Ninja_con.getJump();

        if (this.isClear)
        {
            this.TX_1.text = "Game Clear!";
            this.score += 1000;
            p = 1000;
        }
        else
        {
            this.TX_1.text = "Game Over!";
            p = 100;
        }

        this.TX_2.text = "HP:" + this.nhp;
        this.TX_3.text = "Jump Count:" + this.njp;

        this.score += this.nhp * 100;
        if (this.njp > 0)
        {
            this.score += (int)p / this.njp;
        }

        this.TX_4.text = "Score:" + this.score;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if (Input.GetButtonDown("Submit"))
        {
            Application.Quit();
        }
    }
}
