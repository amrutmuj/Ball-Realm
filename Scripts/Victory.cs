using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    private static Victory instance;
    public static Victory Instance { get { return instance; } }

    public void Start()
    {
        instance = this;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            LevelManager.Instance.BoxVictory();
        }
    }

}
