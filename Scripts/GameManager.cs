using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* The below 2 lines will help us to access te gameobject or we can say this script and 
    the instances anywhere in the other script for different components. */

    private static GameManager instance;
    public static GameManager Instance{get{return instance;}}
    

    public int currentSkinIndex = 0;
    public int currency = 1000;
    public int skinAvailablity = 1;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("CurrentSkinIndex"))
        {
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkinIndex");
            currency = PlayerPrefs.GetInt("Currency");
            skinAvailablity = PlayerPrefs.GetInt("SkinAvailablity");

        }
        else
        {
            Save();
        }
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentSkinIndex", currentSkinIndex);
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("SkinAvailablity", skinAvailablity);

    }
}
