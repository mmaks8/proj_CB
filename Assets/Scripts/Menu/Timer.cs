using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timertext;
    public static float startTime;

    void Start()
    {
        startTime = 120.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float t = startTime - Time.time;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString ("f2");

        timertext.text = minutes + ":" + seconds;
    }
}
