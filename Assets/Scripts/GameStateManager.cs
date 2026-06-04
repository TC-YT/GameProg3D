using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver,
    Continue,
    Restart
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameObject mainMenuUI;
    public GameObject inGameMenuUI;
    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject ContinueUI;

    public GameState currentState { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        //if (currentState == newState) return;

        StartCoroutine(TransitionToState(newState));

        currentState = newState;
    }

    public void ChangeToMainMenu()
    {
        ChangeState(GameState.MainMenu);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void ChangeToPlaying()
    {
        ChangeState(GameState.Playing);
    }

    public void ChangeToContinue()
    {
        ChangeState(GameState.Continue);
    }

    public void ChangeToPause()
    {
        ChangeState(GameState.Pause);
    }

    public void ChangeToGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void ChangeToRestart()
    {
        ChangeState(GameState.Restart);
        ScoreManager.instance.ScoreRestart();
        CoinManager.instance.CoinRestart();
    }

    private IEnumerator TransitionToState(GameState newState)
    {
        if(newState != GameState.MainMenu)
        {
            yield return new WaitForSecondsRealtime(1);
        }

        currentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        HideAllMenu();

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                mainMenuUI.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.Playing:
                inGameMenuUI.SetActive(true);
                Time.timeScale = 1;
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                break;
            case GameState.Continue:
                inGameMenuUI.SetActive(true);
                Time.timeScale = 1;
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                CoinManager.instance.Continue(10);
                break;
            case GameState.Pause:
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0;
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.Restart:
                inGameMenuUI.SetActive(true);
                Time.timeScale = 1;
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                break;
            case GameState.GameOver:
                GameOverUI.SetActive(true);
                if (CoinManager.instance.totalCoins >= 10)
                    {
                    ContinueUI.SetActive(true);
                }
                Time.timeScale = 0;
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
        }
    }

    private void HideAllMenu()
    {
        mainMenuUI.SetActive(false);
        inGameMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
        ContinueUI.SetActive(false);
    }
}
