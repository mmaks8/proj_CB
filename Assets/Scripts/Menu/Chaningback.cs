using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaningback : MonoBehaviour
{
    GameManager level;
    public void Nexxt()
    {
        /*if(level.gameState == GameState.LEVEL_ONE)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("DRY_PLANET");
        }
        if (level.gameState == GameState.LEVEL_TWO)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LUSH_PLANET");
        }
        if (level.gameState == GameState.LEVEL_THREE)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ICE_PLANET");
        }*/
        UnityEngine.SceneManagement.SceneManager.LoadScene("DRY_PLANET");
    }

}
