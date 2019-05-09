using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timertext;
    public float startTime;
    public float t;
    //public float startTime = 120.0f;
    // private bool finnish = false;


    //public void start()
    //
    // {
    //   startTime = 120.0f;
    // }

    //Update is called once per frame
    //public void star()

    //{

        public void Start()
        {
            startTime = 180.0f;
        }


    public void Update()
   
        {

            t = startTime - Time.time;

            if (t < 0.0)
            {

                timertext.color = Color.red;
                startTime += 60.0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(CONSTANTS.GLOBAL.SCENES.GAME_OVER);
                // startTime += 5.0f;
                //t = 5.0f;
                return;

            }

            //float t = startTime - Time.time;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f1");

            timertext.text = minutes + ":" + seconds;
            

        

        // public void ManageScene()
        // {
        //  UnityEngine.SceneManagement.SceneManager.LoadScene ("TimesUP");

        //  }
   }
}