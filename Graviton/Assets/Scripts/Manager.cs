using UnityEngine;
using TMPro;
using System;

public class Manager : MonoBehaviour
{
    private int _level = 1;
    public static Manager Instance;
    private Utilities.GameState _state;
    [SerializeField] private TMP_Text _messagesUI;
    private int _stars = 0;
    public int Stars
    {
        get => _stars;
        set
        {
            _stars = value;
            Debug.Log(_stars);
        }
    }
    

    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _messagesUI.enabled = State == Utilities.GameState.Pause;
        }
    }
        
    

    private void Awake()
    {
        // Instance is null when no Manager has been initialized
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");

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
}
