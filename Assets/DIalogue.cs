using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DIalogue : MonoBehaviour
{
    public Text textDisplay;
    public string[] sentences;
    private int index = 0;
    public float speed;
    public GameObject ContinueButton;


     void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            ContinueButton.SetActive(true);
        }
    }




    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(speed);


        }
    }

    public void NextSentence()
    {

        ContinueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());


        }
        else
        {
            textDisplay.text = "";
            ContinueButton.SetActive(false);
            SceneManager.LoadScene(2);

        }
     }
   

      }









