using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaningback : MonoBehaviour
{
    public void Nexxt()
    {
        Timer.startTime += 10.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("DRY_PLANET");
    }

}
