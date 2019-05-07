using UnityEngine;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager gm = GameManager.Instance; // init the game manager
        gm.OnStateChange += () =>
        {
            GameManager.Instance.StopAllAudio(); // Delegate the callback, called before each new scene
        };
    }

    public void PlayGame()
    {
        GameManager.Instance.SetGameState(GameState.PRACTICE_STORY);
        // UnityEngine.SceneManagement.SceneManager.LoadScene(CONSTANTS.GLOBAL.SCENES.PRACTICE_STORY);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }




}
