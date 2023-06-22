using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickable : Pickable
{
    public int scoreMultiplier = 2;
    private int earlierScore;

    private void Awake()
    {
        boostType = 3;
    }

    public override void OnTriggerWithPlayer(PlayerController player)
    {
        if (player.scoreBoosted) return;

        GameManager.Instance.PlayerBoosted(boostType, pickableTime);

        earlierScore = gameManager.scoreMultiplier;
        gameManager.scoreMultiplier = scoreMultiplier;

        meshRenderer.enabled = false;
        objectCollider.enabled = false;
        player.scoreBoosted = true;

        audioSource.Play();

        StartCoroutine(CountdownToReset(player));

    }

    public override void ResetPlayer(PlayerController player)
    {
        gameManager.scoreMultiplier = earlierScore;
        player.scoreBoosted = false;
    }
}
