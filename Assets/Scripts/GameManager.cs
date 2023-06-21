using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ConstParamenters
{
    public const float COMBO_TIMER_DEFAULT = 5f;
}

public class GameManager : MonoBehaviour
{
    public delegate void OnScoreChanges(int points);
    public event OnScoreChanges OnChangePoints;
    public delegate void OnBoost(int points);
    public event OnBoost OnBoosted;
    public event OnBoost FinishBoosted;

    public float timer;
    [HideInInspector] public float comboTimer = 0;
    [HideInInspector] public int slapCount = 0;
    [HideInInspector] public int slapPointsCount = 0;
    [HideInInspector] public bool isCombo = false;
    private int points;
    private int boostType;
    private float secondsOfBoost;
    private int internalWinningPoints;
    private bool looseByLife;//hay que poner un delegado de player a esto, que setee la perdida por vida
    private bool looseByTime;
    private int numSpeedPickables = 1;
    private int numForcePickables = 1;
    private int numScorePickables = 2;

    public List<Vector2> ListOfAllSpawners;
    public List<Vector2> ListOfOccupiedSpawners;

    public SpeedPickable speedPickable;
    public ForcePickable forcePickable;
    public ScorePickable scorePickable;

    public static GameManager Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        looseByLife = false;
        looseByTime = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            ListOfAllSpawners = new List<Vector2>();
            ListOfOccupiedSpawners = new List<Vector2>();


            ListOfAllSpawners.Add(new Vector2(13, 0));
            ListOfAllSpawners.Add(new Vector2(0, 0));
            ListOfAllSpawners.Add(new Vector2(13, 8));
            ListOfAllSpawners.Add(new Vector2(0, 8));

            if (numSpeedPickables + numForcePickables + numScorePickables <= ListOfAllSpawners.Count)
            {
                SpawnPickables();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) 
        {
            looseByTime = true;
            //DieByTime();
        }

        if(points >= internalWinningPoints) 
        {
            //WinByPoints();
        }

        if (comboTimer > 0)
        {
            Debug.Log("Hay combo!");
            comboTimer -= Time.deltaTime;
        }
        else if (comboTimer < 0 && isCombo)
        {
            Debug.Log("No hay combo!");
            isCombo = false;
            if (slapCount == 0)
            {
                AddPoints(slapPointsCount);
                slapPointsCount = 0;
            }
            else
            {
                AddPoints(slapPointsCount * slapCount);
                slapPointsCount = 0;
                slapCount = 0;
            }
        }
    }
    public void PlayerBoosted(int _BoostType,float _Seconds)
    {
        //speed boost is 1
        //force boost is 2
        //score boost is 3

        boostType = _BoostType;
        secondsOfBoost = _Seconds;
        //OnBoosted(boostType);
        //StartCoroutine(BoostCoroutine());
    }
    IEnumerator BoostCoroutine() 
    {
        yield return new WaitForSeconds(secondsOfBoost);
        boostType = 0;
        secondsOfBoost = 0;
        FinishBoosted(boostType);
    }
   public void AddPoints(int _Points) 
    {
        points += _Points;
        //OnChangePoints(points);
    }
    void DieByLife() 
    {
        SceneManager.LoadScene("LoseScene");

    }
    void DieByTime() 
    {
        SceneManager.LoadScene("LoseScene");
    }
    void WinByPoints() 
    {
        SceneManager.LoadScene("WinScene");
    }



    //PICKABLE SPAWNERS//////////////////////////////////////////////////////////////

    void SpawnPickables()
    {
        for (int i = 0; i<numSpeedPickables; i++)
        {
            SpawnSpeedPickable();
        }
        for (int i = 0; i < numForcePickables; i++)
        {
            SpawnForcePickable();
        }
        for (int i = 0; i < numScorePickables; i++)
        {
            SpawnScorePickable();
        }
    }

    void SpawnSpeedPickable()
    {
        int size = ListOfAllSpawners.Count;
        int arrayPos = Random.Range(0, size);

        if (ListOfOccupiedSpawners.Contains(ListOfAllSpawners[arrayPos]))
        {
            SpawnSpeedPickable();
        }
        else
        {
            Vector3 spawnerPos = new Vector3(ListOfAllSpawners[arrayPos].x, 1, ListOfAllSpawners[arrayPos].y);
            Instantiate<SpeedPickable>(speedPickable, spawnerPos, speedPickable.transform.rotation);
            ListOfOccupiedSpawners.Add(ListOfAllSpawners[arrayPos]);
        }
    }

    void SpawnForcePickable()
    {
        int size = ListOfAllSpawners.Count;
        int arrayPos = Random.Range(0, size);

        if (ListOfOccupiedSpawners.Contains(ListOfAllSpawners[arrayPos]))
        {
            SpawnForcePickable();
        }
        else
        {
            Vector3 spawnerPos = new Vector3(ListOfAllSpawners[arrayPos].x, 1, ListOfAllSpawners[arrayPos].y);
            Instantiate<ForcePickable>(forcePickable, spawnerPos, forcePickable.transform.rotation);
            ListOfOccupiedSpawners.Add(ListOfAllSpawners[arrayPos]);
        }
    }

    void SpawnScorePickable()
    {
        int size = ListOfAllSpawners.Count;
        int arrayPos = Random.Range(0, size);

        if (ListOfOccupiedSpawners.Contains(ListOfAllSpawners[arrayPos]))
        {
            SpawnScorePickable();
        }
        else
        {
            Vector3 spawnerPos = new Vector3(ListOfAllSpawners[arrayPos].x, 1, ListOfAllSpawners[arrayPos].y);
            Instantiate<ScorePickable>(scorePickable, spawnerPos, scorePickable.transform.rotation);
            ListOfOccupiedSpawners.Add(ListOfAllSpawners[arrayPos]);
        }
    }

}
