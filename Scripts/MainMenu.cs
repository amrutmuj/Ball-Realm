using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit.UI;
using JMRSDK.Toolkit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelData
{
    public LevelData(string levelName)
    {
        string data = PlayerPrefs.GetString(levelName);
        
        if (data == "")
            return;

        string[] allData = data.Split('&');
        BestTime = float.Parse(allData[0]);
        SilverTime = float.Parse(allData[1]);
        GoldTime = float.Parse(allData[2]);

    }

    public float BestTime { set; get; }
    public float SilverTime { set; get; }
    public float GoldTime { set; get; }

}

public class MainMenu : MonoBehaviour
{

    private static MainMenu instance;
    public static MainMenu Instance { get { return instance; } }

    public Sprite[] border;     
    public GameObject levelBtnPrefab;
    public GameObject levelBtnContainer;
    public GameObject ShopBtnPrefab;
    public GameObject ShopBtnContainer;

    // Defined for the Use in CUSTOMSKINPLAYER
    public Material ball;

    //TextMeshPro
    public TextMeshProUGUI currencytext;

    private int[] costs = { 0, 500, 1000, 2000 };

    private bool nextLevelLocked = false;

    void Start()
    {
        instance = this;
        CustomSkinPlayer(GameManager.Instance.currentSkinIndex);

        currencytext.text = "Currency: " + GameManager.Instance.currency.ToString();

        //Instantiating the Ball Color Choice & Selection in the Shop Panel

        int textureball = 0;

        // Yaha par sprite isliye banay ha taki ShopSelection me direct hi Sprite aajaye from RESOURCES > PLAYER ...
        Sprite[] textures = Resources.LoadAll<Sprite>("Player");
        foreach (Sprite texture in textures)
        {
            GameObject container = Instantiate(ShopBtnPrefab) as GameObject;
            container.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = texture;
            
            container.transform.SetParent(ShopBtnContainer.transform, false);
            int index = textureball;
            container.GetComponent<JMRUIButton>().onButtonClick.AddListener(() => CustomSkinPlayer(index));
            container.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = costs[index].ToString();
            //making the texture accessible for the player after purchase
            if ((GameManager.Instance.skinAvailablity & 1 << index) == 1 << index)
            {
                container.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                container.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            }
            
            textureball++;

        }

        //Instantiating the Levels in the Level Panel

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelBtnPrefab) as GameObject;
            container.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = thumbnail;
            LevelData level = new LevelData(thumbnail.name);

            //Initating the GameObject to access the Panel of the Timer in levelPanel

            GameObject bottompanel = container.transform.GetChild(1).GetChild(1).gameObject;

            container.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>() .text = (level.BestTime != 0.0f) ? level.BestTime.ToString("f") : "00:00:00";
            container.transform.SetParent(levelBtnContainer.transform, false);

            container.transform.GetChild(1).GetChild(3).GetComponent<Image>().enabled = nextLevelLocked;
            //container.transform.GetComponent<JMRUIPrimaryButton>().interactable = !nextLevelLocked;
            /* To unable the button action when the Level is LOCKED 
             * container.GetComponent<JMRUIPrimaryButton>().OnInteractableChange(!nextLevelLocked);
             */
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
            string SceneName = thumbnail.name;
            container.GetComponent<JMRUIButton>().onButtonClick.AddListener(() => LoadScene(SceneName));

        }
    }

    private void LoadScene(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    public void CustomSkinPlayer(int index)
    {
        // This is tilling & offset of the  PlayerMaterial in te Assests. 

        if ((GameManager.Instance.skinAvailablity & 1 << index) == 1 << index)
        {
            //Kyuki sirf 4 hi texture ko humne sprite me change kiya hai isliye offset ko 1/4 = 0.25f me convert kiya 
            float x = ((int)index % 4) * 0.25f;

            // Kyuki ek i row hai to y ka koi role hi nahi to till will stay 1 only  
            float y = 1;

            //Note: If THERE ARE 2 ROWS THUS THE 0(first row ) will te below one thus it will be started from bottom 

            //This code will help us to changeskincolor of player (Part of SHADER) 
            ball.SetTextureOffset("_MainTex", new Vector2(x, y));

            GameManager.Instance.currentSkinIndex = index;
            GameManager.Instance.Save();

        }

        else
        {
            //Buy Skin
            int cost = costs[index];
            if (GameManager.Instance.currency >= cost)
            {
                GameManager.Instance.currency -= cost;
                GameManager.Instance.skinAvailablity += 1 << index;
                GameManager.Instance.Save();

                //press button to buy if have sufficient amount or cost
                ShopBtnContainer.transform.GetChild(index).GetChild(1).GetChild(1).gameObject.SetActive(false);
                ShopBtnContainer.transform.GetChild(index).GetChild(1).GetChild(2).gameObject.SetActive(false);
                CustomSkinPlayer(index);

                currencytext.text = "Currency: " + GameManager.Instance.currency.ToString();


            }
        }

    }
}

