using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PanelMain;
    public GameObject PanelOptions;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void ButtonStartGame()
    {
        SceneManager.LoadScene("City");
    }

    public void ButtonOptions() 
    {
        PanelMain.SetActive(false);
        PanelOptions.SetActive(true);
    }

    public void OptionsBackButton() 
    {
        PanelMain.SetActive(true);
        PanelOptions.SetActive(false);
    }
  

    public void ButtonQuitGame()
    {
        Application.Quit();
        Debug.Log("Salí");
    }




}
