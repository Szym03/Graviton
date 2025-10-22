using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Manager : MonoBehaviour
{
    private int _level = 1;
    public static Manager Instance;
    private Utilities.GameState _state;
    private TMP_Text _pauseMessage;
    private int _stars = 0;
    [SerializeField] private AudioResource _starAudio;
    [SerializeField] private AudioResource _explosion;
    private AudioSource _source;

    public int Stars
    {
        get => _stars;
        set
        {
            _stars = value;
            _source.resource = _starAudio;
            _source.Play();
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
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        _source = GetComponent<AudioSource>();
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
            State = State == Utilities.GameState.Play ?
                Utilities.GameState.Pause :
                Utilities.GameState.Play;
        }
        Time.timeScale = State == Utilities.GameState.Play ? 1 : 0;
    }

    public void OnPlayerCrashed()
    {
        State = Utilities.GameState.GameOver;
        ResetGame();
        _source.resource = _explosion;
        _source.Play();
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
