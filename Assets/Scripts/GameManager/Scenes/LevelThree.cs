using UnityEngine;
using UnityEngine.UI;

public class LevelThree : Scene
{
    private Text enemiesCount;
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length == 0)
        {
            GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        }
        enemiesCount = GameObject.Find("EnemiesCount").GetComponent<Text>();
        Debug.Log(enemiesCount.text);
        enemiesCount.text = "ENEMIES : " + enemies.Length;
    }

    public override void init()
    {
    }

    public override void restart()
    {
    }
}
