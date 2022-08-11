using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit.UI;
using JMRSDK.Toolkit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JMRSDK.InputModule;

public class PickLevelMainMenu : MonoBehaviour, IBackHandler
{
    [Header("Access to Container")]
    public GameObject levelBtnPrefab;
    public GameObject levelBtnContainer;


    // Start is called before the first frame update
    void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);

        //Instantiating the Levels in the Level Panel

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelBtnPrefab) as GameObject;
            container.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelBtnContainer.transform, false);

            /*
           LevelData level = new LevelData(thumbnail.name);

           //Initating the GameObject to access the Panel of the Timer in levelPanel

           GameObject bottompanel = container.transform.GetChild(1).GetChild(1).gameObject;

           container.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = (level.BestTime != 0.0f) ? level.BestTime.ToString("f") : "00:00:00";
           container.transform.SetParent(levelBtnContainer.transform, false);

           container.transform.GetChild(1).GetChild(3).GetComponent<Image>().enabled = nextLevelLocked;
           //container.transform.GetComponent<JMRUIPrimaryButton>().interactable = !nextLevelLocked;
           /* To unable the button action when the Level is LOCKED 
            * container.GetComponent<JMRUIPrimaryButton>().OnInteractableChange(!nextLevelLocked);

           if (level.BestTime == 0.0f)
           {
               nextLevelLocked = true;
           }

           else if (level.BestTime < level.GoldTime)
           {
               //Gold border
               bottompanel.GetComponentInParent<Image>().sprite = border[0];
           }

           else if (level.BestTime < level.SilverTime)
           {
               //Silver border
               bottompanel.GetComponentInParent<Image>().sprite = border[1];
           }

           else
           {
               //Bronze border
               bottompanel.GetComponentInParent<Image>().sprite = border[2];
           }

           // This is gonna help to move the levels 

           */

            string SceneName = thumbnail.name;
            container.GetComponent<JMRUIButton>().onButtonClick.AddListener(() => LoadScene(SceneName));

        }
    }

    private void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    public void OnBackAction()
    {
        SceneManager.LoadScene("Home");
    }
}
