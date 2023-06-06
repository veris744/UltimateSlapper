using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float timer;
    private int points;
    private int internalWinningPoints;
    private bool looseByLife;//hay que poner un delegado de player a esto, que setee la perdida por vida
    private bool looseByTime;

    public delegate void OnPoints();
    public event OnPoints OnPointsGet;
    public static GameManager Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        looseByLife = false;
        looseByTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) 
        {
            looseByTime = true;
            DieByTime();
        }

        if(points >= internalWinningPoints) 
        {
            WinByPoints();
        }

    }

    void AddPoints(int _Points) 
    {
        points += _Points;
        OnPointsGet();
    }
    void DieByLife() 
    {
        SceneManager.LoadScene("LooseScene");

    }
    void DieByTime() 
    {
        SceneManager.LoadScene("LooseScene");
    }
    void WinByPoints() 
    {
        SceneManager.LoadScene("WinScene");
    }
}
