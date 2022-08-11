using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit.UI;
using JMRSDK.Toolkit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopMainMenu : MonoBehaviour
{
    [Header("Access to Container")]
    public GameObject ShopBtnPrefab;
    public GameObject ShopBtnContainer;

    // Defined for the Use in CUSTOMSKINPLAYER
    public Material ball;

    //TextMeshPro
    public TextMeshProUGUI currencytext;

    private int[] costs = { 0, 500, 1000, 2000 };

    // Start is called before the first frame update
    void Start()
    {
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
