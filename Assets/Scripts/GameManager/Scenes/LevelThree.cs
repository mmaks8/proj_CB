using UnityEngine;
using UnityEngine.UI;

public class LevelThree : Scene
{
    public GameObject bossObj;
    private Text enemiesCount;
    float elapsedTime;
    bool isBossSpawned;

    private void Start()
    {
        elapsedTime = 0.0f;
        isBossSpawned = false;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] boss = GameObject.FindGameObjectsWithTag("Boss");

        if(enemies.Length == 0 && isBossSpawned == false)
        {
            Instantiate(bossObj, transform.position, transform.rotation);
            elapsedTime = 0.0f;
            isBossSpawned = true;
        }
        if (boss.Length == 0 && enemies.Length == 0 && elapsedTime >= 5f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(CONSTANTS.GLOBAL.SCENES.ENDING);
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
