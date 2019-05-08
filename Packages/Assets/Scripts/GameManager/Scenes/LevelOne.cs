using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// First level. TODO: We should manage the logic of the level (when the player success the level ? etc ..)
/// </summary>
public class LevelOne : Scene
{
    private Text enemiesCount;
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length == 0  )
        {
            GameManager.Instance.SetGameState(GameState.LEVEL_TWO);
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
