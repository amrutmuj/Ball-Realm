using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using TMPro;

public class LevelOneScoreBoard : MonoBehaviour
{
    public Rigidbody controller;
    public TextMeshProUGUI scoreboard;
    int score;
    bool isGameOver;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
            score++;
            scoreboard.text = "SCORE: " + score;


        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            //Game Over
            isGameOver = true;
            controller.velocity = Vector3.zero;
            controller.isKinematic = true;
            Destroy(other.gameObject, 1.0f);
            LevelManager.Instance.gameOver.SetActive(true);
        }
    }
}
