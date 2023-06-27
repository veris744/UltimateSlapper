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
    public delegate void OnLifeChanges(int life);
    public event OnLifeChanges OnChangeLife;
    public delegate void OnBoost(int BoostType);
    public event OnBoost OnBoosted;
    public event OnBoost FinishBoosted;

    public float timer;
    [HideInInspector] public float comboTimer = 0;
    [HideInInspector] public int slapCount = 0;
    [HideInInspector] public int slapPointsCount = 0;
    [HideInInspector] public bool isCombo = false;
    [HideInInspector] public int scoreMultiplier;
    private int points;
    [SerializeField] private int life;
    private int boostType;
    private float secondsOfBoost;
    [SerializeField] private int internalWinningPoints;
    private bool itsPlayableLevel = false;

    private int numSpeedPickables = 8;
    private int numForcePickables = 8;
    private int numScorePickables = 8;
    private int numLifePickables = 8;

    public List<Vector2> ListOfAllSpawners;
    public List<Vector2> ListOfOccupiedSpawners;

    public SpeedPickable speedPickable;
    public ForcePickable forcePickable;
    public ScorePickable scorePickable;
    public LifePickable lifePickable;

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
        scoreMultiplier = 1;
        life = 5;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            ListOfAllSpawners = new List<Vector2>();
            ListOfOccupiedSpawners = new List<Vector2>();


            ListOfAllSpawners.Add(new Vector2(0, 5));
            ListOfAllSpawners.Add(new Vector2(25, 5));
            ListOfAllSpawners.Add(new Vector2(50, 5));
            ListOfAllSpawners.Add(new Vector2(75, 5));
            ListOfAllSpawners.Add(new Vector2(100, 5));
            ListOfAllSpawners.Add(new Vector2(-25, 5));
            ListOfAllSpawners.Add(new Vector2(-50, 5));
            ListOfAllSpawners.Add(new Vector2(-75, 5));
            ListOfAllSpawners.Add(new Vector2(-100, 5));

            ListOfAllSpawners.Add(new Vector2(0, 47));
            ListOfAllSpawners.Add(new Vector2(25, 47));
            ListOfAllSpawners.Add(new Vector2(50, 47));
            ListOfAllSpawners.Add(new Vector2(75, 47));
            ListOfAllSpawners.Add(new Vector2(100, 47));
            ListOfAllSpawners.Add(new Vector2(-25, 47));
            ListOfAllSpawners.Add(new Vector2(-50, 47));
            ListOfAllSpawners.Add(new Vector2(-75, 47));
            ListOfAllSpawners.Add(new Vector2(-100, 47));

            ListOfAllSpawners.Add(new Vector2(0, -81));
            ListOfAllSpawners.Add(new Vector2(25, -81));
            ListOfAllSpawners.Add(new Vector2(50, -81));
            ListOfAllSpawners.Add(new Vector2(75, -81));
            ListOfAllSpawners.Add(new Vector2(100, -81));
            ListOfAllSpawners.Add(new Vector2(-25, -81));
            ListOfAllSpawners.Add(new Vector2(-50, -81));
            ListOfAllSpawners.Add(new Vector2(-75, -81));
            ListOfAllSpawners.Add(new Vector2(-100, -81));

            ListOfAllSpawners.Add(new Vector2(120, 90));
            ListOfAllSpawners.Add(new Vector2(60, 90));
            ListOfAllSpawners.Add(new Vector2(28, 90));
            ListOfAllSpawners.Add(new Vector2(-119, 90));
            ListOfAllSpawners.Add(new Vector2(87, 90));

            ListOfAllSpawners.Add(new Vector2(0, 129));
            ListOfAllSpawners.Add(new Vector2(50, 129));
            ListOfAllSpawners.Add(new Vector2(100, 129));
            ListOfAllSpawners.Add(new Vector2(-50, 129));
            ListOfAllSpawners.Add(new Vector2(-100, 129));

            if (numSpeedPickables + numForcePickables + numScorePickables <= ListOfAllSpawners.Count)
            {
                SpawnPickables();
            }
            itsPlayableLevel = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itsPlayableLevel)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                DieByTime();
            }
        }
        if (life <= 0)
        {
            DieByLife();
        }

        if (points >= internalWinningPoints)
        {
            WinByPoints();
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
    public void PlayerBoosted(int _BoostType, float _Seconds)
    {
        //speed boost is 1
        //force boost is 2
        //score boost is 3
        if (boostType != 0)
        {
            return;
        }
        boostType = _BoostType;
        secondsOfBoost = _Seconds;
        OnBoosted(boostType);
        StartCoroutine(BoostCoroutine());
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
        if (_Points < 0)
            points += _Points;
        else
            points += (scoreMultiplier * _Points);

        OnChangePoints(points);
    }
    public int GetLifes()
    {
        return life;
    }
    public void AddLifes(int _Life)
    {
        life += _Life;
        OnChangeLife(life);
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
        for (int i = 0; i < numSpeedPickables; i++)
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
        for (int i = 0; i < numLifePickables; i++)
        {
            SpawnLifePickable();
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


    void SpawnLifePickable()
    {
        int size = ListOfAllSpawners.Count;
        int arrayPos = Random.Range(0, size);

        if (ListOfOccupiedSpawners.Contains(ListOfAllSpawners[arrayPos]))
        {
            SpawnLifePickable();
        }
        else
        {
            Vector3 spawnerPos = new Vector3(ListOfAllSpawners[arrayPos].x, 1, ListOfAllSpawners[arrayPos].y);
            Instantiate<LifePickable>(lifePickable, spawnerPos, lifePickable.transform.rotation);
            ListOfOccupiedSpawners.Add(ListOfAllSpawners[arrayPos]);
        }
    }
}
