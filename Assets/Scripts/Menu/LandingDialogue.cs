using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingDialogue : MonoBehaviour
{
    public Text textDisplay1;
    public string[] sentences1;
    private int index1 = 0;
    public float speed1;
    public GameObject ContinueButton1;


    void Start()
    {
        StartCoroutine(Type1());
    }

    void Update()
    {
        if (textDisplay1.text == sentences1[index1])
        {
            ContinueButton1.SetActive(true);
        }
    }




    IEnumerator Type1()
    {
        foreach (char letter in sentences1[index1].ToCharArray())
        {
            textDisplay1.text += letter;
            yield return new WaitForSeconds(speed1);


        }
    }

    public void NextSentence1()
    {

        ContinueButton1.SetActive(false);

        if (index1 < sentences1.Length - 1)
        {
            index1++;
            textDisplay1.text = "";
            StartCoroutine(Type1());


        }
        else
        {
            textDisplay1.text = "";
            ContinueButton1.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }


}









