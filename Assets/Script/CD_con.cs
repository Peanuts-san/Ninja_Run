using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CD_con : MonoBehaviour
{
    Manager_con manager;
    string txt;
    public GameObject cd;

    // Start is called before the first frame update
    void Start()
    {
        manager = (GameObject.Find("Manager")).GetComponent<Manager_con>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string s)
    {
        this.gameObject.GetComponent<Text>().text = s;
    }
}
