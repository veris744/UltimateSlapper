using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonRetry()
    {
        GameManager.Instance.RestartLevel();
        SceneManager.LoadScene("City");
    }
    public void ButtonQuit()
    {
        GameManager.Instance.RestartLevel();
        SceneManager.LoadScene("MainMenu");
    }
}
