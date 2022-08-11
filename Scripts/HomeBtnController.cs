using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.Toolkit;
using JMRSDK.InputModule;
using UnityEngine.SceneManagement;


public class HomeBtnController : MonoBehaviour, IBackHandler,IHomeHandler, IFocusable
{
    [Header("Button")]
    public JMRUIPrimaryButton PlayBtn;
    public JMRUIPrimaryButton InstructionBtn;
    public JMRUIPrimaryButton StoryBtn;
    //public JMRUIPrimaryButton ResetBtn;
    public JMRUIPrimaryButton ShopBtn;
    public JMRUIPrimaryButton SettingBtn;
    public JMRUIPrimaryButton LeftInstBtn;
    public JMRUIPrimaryButton RightInstBtn;


    [Header("Panels")]
    public GameObject HomeMenuPanel;
    public GameObject InstructionMenuPanel;
    public GameObject StoryMenuPanel;
    public GameObject ShopMenuPanel;
    public GameObject SettingMenuPanel;
    public GameObject One_InstructionPanel;
    public GameObject Two_InstructionPanel;
    


    private void Awake()
    {
        InstructionMenuPanel.SetActive(false);
        HomeMenuPanel.SetActive(true);
        StoryMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);
        SettingMenuPanel.SetActive(false);
        One_InstructionPanel.SetActive(true);
        Two_InstructionPanel.SetActive(false);
        
    }

    void Start()
    {
        JMRInputManager.Instance.AddGlobalListener(gameObject);
        PlayBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => SceneManager.LoadScene("PickLevel"));
        InstructionBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => InstructionBtnAction());
        StoryBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => StoryBtnAction());
        //ResetBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => ResetBtnAction());
        ShopBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => ShopBtnAction());
        SettingBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => SettingBtnAction());
        LeftInstBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => Two_InstBtnAction());
        RightInstBtn.GetComponent<JMRUIPrimaryButton>().OnClick.AddListener(() => One_InstBtnAction());
    }

    /*public void ResetBtnAction()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/

    public void OnFocusEnter()
    {
        Debug.Log("OnFocusEnter");
    }
    public void OnFocusExit()
    {
        Debug.Log("OnFocusExit");
    }

    public void One_InstBtnAction()
    {
        One_InstructionPanel.SetActive(false);
        Two_InstructionPanel.SetActive(true);
    }
    
    public void Two_InstBtnAction()
    {
        One_InstructionPanel.SetActive(true);
        Two_InstructionPanel.SetActive(false);
    }

    public void InstructionBtnAction()
    {
        InstructionMenuPanel.SetActive(true);
        HomeMenuPanel.SetActive(false);
        StoryMenuPanel.SetActive(false);
        SettingMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);
    }
    
    public void StoryBtnAction()
    {
        InstructionMenuPanel.SetActive(false);
        HomeMenuPanel.SetActive(false);
        StoryMenuPanel.SetActive(true);
        SettingMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);
    }

    public void ShopBtnAction()
    {
        ShopMenuPanel.SetActive(true);
        HomeMenuPanel.SetActive(false);
    }

    public void SettingBtnAction()
    {
        SettingMenuPanel.SetActive(true);
        InstructionMenuPanel.SetActive(false);
        HomeMenuPanel.SetActive(false);
        StoryMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);

    }

    public void BacktoHomeMENU()
    {
        InstructionMenuPanel.SetActive(false);
        HomeMenuPanel.SetActive(true);
        StoryMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);
        SettingMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false);

    }

    public void OnBackAction()
    {
        Debug.Log("OnBackAction");
        BacktoHomeMENU();
    }
    
    public void OnHomeAction()
    {
        Debug.Log("OnHomeAction");
        BacktoHomeMENU();
    }   
    
}
