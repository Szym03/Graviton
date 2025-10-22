using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private int _level = 1;
    public static Manager Instance;
    private Utilities.GameState _state;
    private TMP_Text _pauseMessage;
    private int _stars = 0;
    public int Stars
    {
        get => _stars;
        set
        {
            _stars = value;
            if (Stars == 3)
            {
                _level += 1;
                NextLevel();
            }
        }
    }
    

    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _pauseMessage.enabled = State == Utilities.GameState.Pause;
        }
    }
        
    

    private void Awake()
    {
        // Instance is null when no Manager has been initialized
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");
             SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }

        // We evaluate this portion when trying to initialize a new instance
        // when one already exists
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }

    private void Start()
    {
        _pauseMessage = GameObject.Find("Pause").GetComponent<TMP_Text>();
        State = Utilities.GameState.Play;
        _level = 1;
        
    }

    private void Update()
    {
        // ez pausing
        if (Input.GetKeyDown(KeyCode.P))
        {
            State = State == Utilities.GameState.Play  ?
                Utilities.GameState.Pause :
                Utilities.GameState.Play;
        }
        Time.timeScale = State == Utilities.GameState.Play ? 1 : 0;
    }

    public void OnPlayerCrashed()
    {
        State = Utilities.GameState.GameOver;
        ResetGame();
    }

    private void ResetGame()
    {
        Stars = 0;
        switch (_level)
        {
            case 1:
                SceneManager.LoadScene("Level 1");
                break;
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            default:
                SceneManager.LoadScene("Home");
                break;
        }

        State = Utilities.GameState.Play;

    }

    private void NextLevel()
    {
        SceneManager.LoadScene(_level);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPauseUI();
        State = Utilities.GameState.Play;
    }

    private void FindPauseUI()
    {
        var pauseObj = GameObject.Find("Pause");
        if (pauseObj != null)
            _pauseMessage = pauseObj.GetComponent<TMP_Text>();
    }
}
