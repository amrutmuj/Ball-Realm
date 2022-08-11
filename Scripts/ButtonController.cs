using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit;
using JMRSDK.InputModule;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour, IBackHandler, IHomeHandler
{

    public JMRUIPrimaryButton Level;
    public JMRUIPrimaryButton Shop;
    public GameObject ShopPanel;
    public GameObject LevelPanel;
    public GameObject MainMENU;

   
    // Start is called before the first frame update
    private void Awake()
    {
        ShopPanel.SetActive(false); 
        MainMENU.SetActive(true);
        LevelPanel.SetActive(false);
    }

    void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
        Level.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => LevelPanelActivation());
        Shop.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => ShopPanelActivation());
    }

    void Update()
    {
        
        /*
         * Intially this was the Back Button
         * 
         BackbtnShop.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => BacktoMainMenu());
        BackbtnLevel.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => BacktoMainMenu());
        
         */
    }

    public void ShopPanelActivation()
    {
        ShopPanel.SetActive(true);
        MainMENU.SetActive(false);
        LevelPanel.SetActive(false);

    }

    public void LevelPanelActivation()
    {
        ShopPanel.SetActive(false);
        MainMENU.SetActive(false);
        LevelPanel.SetActive(true);

    }
    
    public void BacktoMainMenu()
    {
        ShopPanel.SetActive(false);
        MainMENU.SetActive(true);
        LevelPanel.SetActive(false);
    }

    public void OnBackAction()
    {
        Debug.Log("OnBackAction");
        BacktoMainMenu();

    }
    
    public void OnHomeAction()
    {
        Debug.Log("OnHomeAction");
        SceneManager.LoadScene("Home");

    }
}
