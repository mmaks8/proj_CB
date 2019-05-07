using System.Xml;
using UnityEngine;

/// <summary>
/// First level. TODO: We should manage the logic of the level (when the player success the level ? etc ..)
/// </summary>
public class LevelOne : Scene
{
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length == 0)
        {
            GameManager.Instance.SetGameState(GameState.LEVEL_TWO);
        }
    }

    public override void init()
    {
    }

    public override void restart()
    {
    }
}
