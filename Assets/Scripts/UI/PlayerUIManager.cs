using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{

    public GameObject [] boosts;
    public TMP_Text lifeText;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnChangePoints += ChangePoints;
        GameManager.Instance.OnChangeLife += ChangeLife;
        GameManager.Instance.OnBoosted += ChangeBoost;
        GameManager.Instance.FinishBoosted += FinishBoost;
        foreach (var item in boosts)
        {
            item.SetActive(false);
        }
        lifeText.text = GameManager.Instance.GetLifes().ToString();

    }

    void ChangePoints(int _points)
    {
        scoreText.text = "Score: " + _points;
    }
    void ChangeLife(int _Life)
    {
        lifeText.text = _Life.ToString();
    }
    void ChangeBoost(int _boostType)
    {
        int boostType = _boostType - 1;
        if(boostType <= boosts.Length)
        boosts[boostType].SetActive(true);
    }
    void FinishBoost(int _boostType)
    {
        foreach (var item in boosts)
        {
            item.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
