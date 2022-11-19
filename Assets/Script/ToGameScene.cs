using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameScene : MonoBehaviour
{
    new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //SpaceÇ≈ÉVÅ[ÉìêÿÇËë÷Ç¶
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine("Lets");
        }
    }

    IEnumerator Lets()
    {
        this.audio.Play();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameScene");
    }
}
