using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToGameScene : MonoBehaviour
{
    new AudioSource audio;

    Text tx;
    public GameObject pq = null;

    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>();
        this.tx = this.pq.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //SpaceÇ≈ÉVÅ[ÉìêÿÇËë÷Ç¶
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine("Lets");
        }
        else if (Input.GetButtonDown("Submit"))
        {
            this.tx.text = "       Quit:A";
            Application.Quit();
        }
    }

    IEnumerator Lets()
    {
        this.audio.Play();
        this.tx.text = "Play:B       ";
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameScene");
    }
}
