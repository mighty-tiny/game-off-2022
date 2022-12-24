using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    /* SERIALIZED FIELDS */
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStart;
    [SerializeField] private GameObject[] gameLevelArr;

    /* GAME UI TEXT */
    public TextMeshProUGUI collectiblesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI livesText;

    /* GAME UI PANELS */
    public GameObject uiMenuPanel;
    public GameObject gamePlayPanel;
    public GameObject levelTextPanel;
    public GameObject victoryPanel;
    public GameObject levelCompletePanel;

    
    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value;
            // levelText.text = "Level " + _level;
        }
    }
    

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            // scoreText.text = "SCORE: " + _score;
        }
    }

    private int _lives;
    public int Lives
    {
        get { return _lives; }
        set { _lives = value;
            // livesText.text = "Lives: " + _lives;
        }
    }

    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value;
            // healthText.text = "HP: " + _health;
        }
    }

    public enum State { START, MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER }
    State _state;

    void Start()
    {
        SwitchState(State.MENU);
        SubscribeToGameEvents();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.START:
                break;
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }

    public void SwitchState(State newState)
    {
        EndState();
        BeginState(newState);
    }
    private void BeginState(State newState)
    {
        // uiMenuPanel, gamePlayPanel, levelTextPanel, gameOverPanel, levelCompletePanel
        switch (newState)
        {
            case State.START:
                break;
            case State.MENU:
                // uiMenuPanel.SetActive(true);
                break;
            case State.INIT:
                // uiMenuPanel.SetActive(false);
                // gamePlayPanel.SetActive(true);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                // levelCompletePanel.SetActive(true);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                victoryPanel.SetActive(true);
                break;
        }
    }

    private void EndState()
    {
        switch (_state)
        {
            case State.START:
                break;
            case State.MENU:
                // uiMenuPanel.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                // gamePlayPanel.SetActive(false);
                // victoryPanel.SetActive(true);
                break;
        }
    }
    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    public void victoryCheck()
    {
        if (_score >= 100)
        {
            SwitchState(State.GAMEOVER);
        }
    }

    private void SubscribeToGameEvents() {
        GameEvents.current.onPickupCollected += OnPickupCollected;
    }

    private void OnPickupCollected(int pointValue)
    {
        Score += pointValue;
        Debug.Log("Score has increased by " + pointValue +  " points!");
        Debug.Log("Total Score: " + _score);
    }

    private void FocusOnGame(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
