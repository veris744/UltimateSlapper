using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject UIPanel;
    public GameObject PausePanel;
    public GameObject PanelOptions;
    public static UIManager current; 
    public bool IsPaused { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (this.IsPaused)
            {
                this.ButtonResumeGame();
            }
            
            else
            {
                this.Pause();
            }
        }
    }

    private void Awake()
    {
        current = this;
        this.ButtonResumeGame();
    }

    public void Pause()
    {
        this.IsPaused = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        UIPanel.SetActive(false);
    }
    
    public void ButtonResumeGame()
    {
        //Falta la tecla p y despausar :v
        this.IsPaused = false;
        Time.timeScale = 1;
        PanelOptions.SetActive(false);
        PausePanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    public void ButtonOptions()
    {
        PausePanel.SetActive(false);
        PanelOptions.SetActive(true);
    }

    public void OptionsBackButton()
    {
        PanelOptions.SetActive(false);
        PausePanel.SetActive(true);
    }


    public void ButtonQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
