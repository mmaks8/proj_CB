using UnityEngine;

/// <summary>
/// Sound Manager, should be used only by the GM.
/// </summary>
class SoundManager : MonoBehaviour 
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager Instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    void Awake ()
    {
        //Check if there is already an instance of SoundManager
        if (Instance == null)
            //if not, set it to this.
            Instance = this;
        //If instance already exists:
        else if (Instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy (gameObject);
        
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);
    }
    
    
    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;
        
        //Play the clip.
        efxSource.Play ();
    }
    
    
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
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

    // Stop all audio sounds playing currently
    public void StopAllAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource  audioSource in allAudioSources)
        {
            audioSource.Stop();
        }
    }
}

/// <summary>
/// Differents game states of the game
/// </summary>
public enum GameState
{
    MAIN_MENU,
    INTRO,
    LEVEL_ONE,
    LEVEL_TWO,
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
 
    // Call this to get the instance
    public static GameManager Instance {
        get {
            if (instance == null)
            {
                DontDestroyOnLoad(instance);
                instance = new GameManager();
            }

            return instance;
        }
    }
    
    public void SetGameState(GameState state){
        this.gameState = state;
        OnStateChange();
    }

    // Add your game manager members here
    public void Pause(bool paused) {
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
        SoundManager.Instance.PlaySingle(clip);
    }
    
    public void RandomizeSfx(AudioClip[] clips)
    {
        SoundManager.Instance.RandomizeSfx(clips);
    }
    
    public void StopAllAudio()
    {
        SoundManager.Instance.StopAllAudio();
    }
}