using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using JMRSDK.InputModule;

public class LevelManager : MonoBehaviour, IBackHandler, IHomeHandler
{
    private static LevelManager instance;
    public static LevelManager Instance {get { return instance; } }
    
    public GameObject pauseMenu;
    public GameObject gameOver;
    public Rigidbody PlayerEntry;
    private bool isGameOver = false;

    private bool flagBall;
    private float startTime;
    public float GOLDTIME = 8.0f;
    public float SILVERTIME = 10.0f;

    [Header("Congratulations Panel")]
    public GameObject congratulationsMenu;


    private void Start()
    {
        instance = this;
        flagBall = false;
        congratulationsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        startTime = Time.time;
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }

    //Back Btn from Controller will gonna access this
    public void OnBackAction()
    {
        Debug.Log("OnBackAction");
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        //While I'm in pause Mode the Ball will be at HALT.

        if (flagBall == false)
        {
            PlayerEntry.velocity = Vector3.zero;
            PlayerEntry.isKinematic = true;
            flagBall = true;
        }

        else
        {
            PlayerEntry.isKinematic = false;
            flagBall = false;
        }
    }

    /*public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }*/
    
    public void OnHomeAction()
    {
        if (BallController.Instance.Equals(isGameOver) == false)
        {
            pauseMenu.SetActive(true);
            gameOver.SetActive(false);
            flagBall = true;
            PlayerEntry.velocity = Vector3.zero;
            PlayerEntry.isKinematic = true;
        }

        else if (BallController.Instance.Equals(isGameOver) == true)
        {
            HomeActionOnGameOver();
        }
        
    }

    public void HomeActionOnGameOver()
    {
        Destroy(pauseMenu);
        gameOver.SetActive(true);
        flagBall = true;
        PlayerEntry.velocity = Vector3.zero;
        PlayerEntry.isKinematic = true;
    }

    public void OnMenuAction()
    {
        SceneManager.LoadScene("PickLevel");
        BallController.Instance.scorereset();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //BallController.Instance.scoreboard.text = "SCORE: ";
        BallController.Instance.scorereset();
    }

    public void BoxVictory()
    {
        Debug.Log("Win");

        /* float duration = Time.time - startTime;
        if (duration < GOLDTIME)
        {
            GameManager.Instance.currency += 300;
        }
        else if (duration < SILVERTIME)
        {
            GameManager.Instance.currency += 200;
        }
        else if (duration > 10.0f)
        {
            GameManager.Instance.currency += 100;
        }
                
        GameManager.Instance.Save();

        string saveString = "";
        // "30&60&45"
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        saveString += (level.BestTime > duration || level.BestTime == 0.0f) ? duration.ToString() : level.BestTime.ToString();
        saveString += '&';
        saveString += SILVERTIME.ToString();
        saveString += '&';
        saveString += GOLDTIME.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString); */

        BallController.Instance.GameWonAction();
        PlayerEntry.velocity = Vector3.zero;
        PlayerEntry.isKinematic = true;
        congratulationsMenu.SetActive(true);
        AudioSourceScript.Instance.AudioPlayerHitsEnemy();
        Destroy(pauseMenu);
        Destroy(gameOver);
    }
}
 