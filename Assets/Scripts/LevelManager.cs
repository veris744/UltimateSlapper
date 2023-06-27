using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnChangeLife += CheckLife;
        GameManager.Instance.OnChangePoints += CheckPoints;
        GameManager.Instance.TimerFinish += CheckTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckLife(int _Life)
    {
        if (_Life <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
    void CheckPoints(int _Points)
    {
        if (_Points >= GameManager.Instance.GetInternalWinningPoints())
        {
            SceneManager.LoadScene("WinScene");
        }

    }
    void CheckTime()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
