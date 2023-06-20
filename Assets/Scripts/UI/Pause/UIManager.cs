using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject UIPanel;
    public GameObject PausePanel;
    public GameObject PanelOptions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonResumeGame()
    {
        //Falta la tecla p y despausar :v
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
