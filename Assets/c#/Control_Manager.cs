using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Manager : MonoBehaviour
{
    float time = 0;
    public bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if ((time / 60) == 0)
        {
            Debug.Log(time / 60);
        }
        if (time >= 180)
        {
            isStart = true;
        }
    }
}
