using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timertext;
    static  public float startTime = 50.0f;
    public float t;
    //public string Scenemane;
  //  public string currentscene;
    //public float startTime = 120.0f;
    // private bool finnish = false;


  

       // public void Start()

      //  {
      //  currentscene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
       // startTime = 10.0f;
      // Scenemane = currentscene.name;
      //  }


    public void Update()
   
        {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("DRY_PLANET") || UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("TimesUP"))
        {
           
            t = startTime - Time.time;
            //startTime += 10.0f;
            if (t < 0.0)
            {
               // startTime += 60.0f
                timertext.color = Color.red;
              // startTime += 60.0f;
                UnityEngine.SceneManagement.SceneManager.LoadScene("TimesUP");
                // startTime += 5.0f;
                //t = 5.0f;
                return;

            }

            //float t = startTime - Time.time;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f1");

            timertext.text = minutes + ":" + seconds;

        }
        

        // public void ManageScene()
        // {
        //  UnityEngine.SceneManagement.SceneManager.LoadScene ("TimesUP");

        //  }
   }
}