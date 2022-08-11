using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JMRSDK.InputModule;
using JMRSDK;
using TMPro;

public class BallController : MonoBehaviour
{
    private static BallController instance;
    public static BallController Instance { get { return instance; } }

    /*
    
    
    public void OnTouchStart(TouchEventData eventData, Vector2 TouchData)
    {
        Debug.Log("OnTouchStarted " + TouchData.ToString());
        if (TouchData.x < 0.5f && TouchData.y <= 0.5f)
        {
            Debug.Log("Move Spaceship Left!"); 
            controller.velocity = new Vector3(-moveSpeed * 1, 0, 0);
        }
    }
    public void OnTouchStop(TouchEventData eventData, Vector2 TouchData)
    {
        Debug.Log("OnTouchStop " + TouchData.ToString());
    }
    public void OnTouchUpdated(TouchEventData eventData, Vector2 TouchData)
    {
        Debug.Log("OnTouchUpdated " + TouchData.ToString());
    }

    */

    /*   public void OnSwipeCanceled(SwipeEventData eventData)
        {
            Debug.Log("OnSwipeCanceled");
        }
        public void OnSwipeCompleted(SwipeEventData eventData)
        {
            Debug.Log("OnSwipeCompleted");
        }
        public void OnSwipeDown(SwipeEventData eventData, float delta)
        {
            Debug.Log("OnSwipeDown");
            controller.velocity = new Vector3(0, 0, -moveSpeed * 1);
        }
        public void OnSwipeLeft(SwipeEventData eventData, float delta)
        {
            Debug.Log("OnSwipeLeft");
            controller.velocity = new Vector3(-moveSpeed * 1, 0, 0);
        }
        public void OnSwipeRight(SwipeEventData eventData, float delta)
        {
            Debug.Log("OnSwipeRight");
            controller.velocity = new Vector3(moveSpeed * 1, 0, 0);
        }
        public void OnSwipeStarted(SwipeEventData eventData)
        {
            Debug.Log("OnSwipeStarted");
        }
        public void OnSwipeUp(SwipeEventData eventData, float delta)
        {
            Debug.Log("OnSwipeUp");
            controller.velocity = new Vector3(0, 0, moveSpeed * 1);
        }
        public void OnSwipeUpdated(SwipeEventData eventData, Vector2 delta)
        {
            Debug.Log("OnSwipeUpdated");
        }
    */

    public float moveSpeed = 2.0f;
    private Rigidbody controller;
    bool isGameOver;

    public TextMeshProUGUI scoreboard;
    public TextMeshProUGUI CongoScoreBoard;
    int score;


    //Actions

    private void Start()
    {
        instance = this;
        controller = GetComponent<Rigidbody>();
        
    }

    public void Update()
    {
        bool isSwipeUp = JMRInteraction.GetSwipeUp(out float val);
        bool isSwipeDown = JMRInteraction.GetSwipeDown(out float down);
        bool isSwipeLeft = JMRInteraction.GetSwipeLeft(out float leftside);
        bool isSwipeRight = JMRInteraction.GetSwipeRight(out float rightside);

        if (!isGameOver)
        {
            if (isSwipeUp)
            {
                Debug.Log("Up");
                Vector3 movement = new Vector3(0, 0, 2.0f);
                controller.AddForce(movement * moveSpeed);
            }

            if (isSwipeDown)
            {
                Debug.Log("Down");
                Vector3 movement = new Vector3(0, 0, -2.0f);
                controller.AddForce(movement * moveSpeed);
            }

            if (isSwipeLeft)
            {
                Debug.Log("Down");
                Vector3 movement = new Vector3(-1.25f, 0, 0);
                controller.AddForce(movement * moveSpeed);
            }

            if (isSwipeRight)
            {
                Debug.Log("Down");
                Vector3 movement = new Vector3(1.25f, 0, 0);
                controller.AddForce(movement * moveSpeed);
            }

        }

    }

    public void scorereset()
    {
        score = 0;
    }

    public void GameWonAction()
    {
        CongoScoreBoard.text = "Your Score : " + score;
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coins")
        {
            Destroy(other.gameObject);
            score++;
            scoreboard.text = "SCORE: " + score;
            
        }

        if (other.gameObject.tag == "Enemy")
        {
            //Game Over
            isGameOver = true;
            controller.velocity = Vector3.zero;
            controller.isKinematic = true;
            Destroy(other.gameObject, 1.0f);
            LevelManager.Instance.HomeActionOnGameOver();
            score = 0;
            scoreboard.text = "SCORE: " + score;
            AudioSourceScript.Instance.AudioPlayerHitsEnemy();

        }
    }

}
    