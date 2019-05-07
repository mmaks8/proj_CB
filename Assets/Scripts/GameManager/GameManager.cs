using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Differents game states of the game
/// </summary>
public enum GameState
{
    MAIN_MENU,
    PRACTICE_STORY,
    INTRO_LEVEL_ONE,
    LEVEL_ONE,
    INTRO_LEVEL_TWO,
    LEVEL_TWO,
    INTRO_LEVEL_THREE,
    LEVEL_THREE
}

// Callback
public delegate void OnStateChangeHandler();

/// <summary>
/// Game Manager. Used for managing everything related to the core of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public event OnStateChangeHandler OnStateChange; // callback
    private GameManager() {}    
    private static GameManager instance;
    // Game Stats
    private int Score { get; set; }
    private static Dictionary<GameState, string> scenes = new Dictionary<GameState, string>();
    // Sfx stuff
    private static AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    private static AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    private static float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    private static float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    // Call this to get the instance
    public static GameManager Instance {
        get {
            if (instance == null)
            {
                GameObject go = new GameObject("_gamemanager");
                DontDestroyOnLoad(go);
                instance = go.AddComponent<GameManager>();
                instance.gameState = GameState.MAIN_MENU;

                scenes.Add(GameState.MAIN_MENU, CONSTANTS.GLOBAL.SCENES.MENU);
                scenes.Add(GameState.PRACTICE_STORY, CONSTANTS.GLOBAL.SCENES.PRACTICE_STORY);
                scenes.Add(GameState.INTRO_LEVEL_ONE, CONSTANTS.GLOBAL.SCENES.DRY_PLANET_INTRO);
                scenes.Add(GameState.LEVEL_ONE, CONSTANTS.GLOBAL.SCENES.DRY_PLANET);
                // scenes.Add(GameState.INTRO_LEVEL_TWO, CONSTANTS.GLOBAL.SCENES.LUSH_PLANET_INTRO);
                scenes.Add(GameState.LEVEL_TWO, CONSTANTS.GLOBAL.SCENES.LUSH_PLANET);
                // scenes.Add(GameState.INTRO_LEVEL_THREE, CONSTANTS.GLOBAL.SCENES.ICE_PLANET_INTRO);
                // scenes.Add(GameState.LEVEL_THREE, CONSTANTS.GLOBAL.SCENES.ICE_PLANET);
            }
            return instance;
        }
    }
    
    public void SetGameState(GameState state) {
        this.gameState = state;

        OnStateChange?.Invoke();

        if (scenes.ContainsKey(this.gameState))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[this.gameState]);
        }
        else
        {
            throw new Exception("[-] Invalid game state");
        }
    }

    // Add your game manager members here
    public void Pause(bool paused) {
        // TODO
    }

    /// <summary>
    /// Display a text on the screen
    /// </summary>
    public void DisplayMessage(/* TODO: define parameters there*/)
    {
        
    }
    
    /// <summary>
    /// SFX METHODS
    /// </summary>
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;
        
        //Play the clip.
        efxSource.Play ();
    }
    
    public void RandomizeSfx(AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);
        
        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        
        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;
        
        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];
        
        //Play the clip.
        efxSource.Play();
    }
    
    public void StopAllAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource  audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }
}