using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickable : Pickable
{
    public float scoreMultiplier = 2;
    private float earlierScore;


    public override void OnTriggerWithPlayer(PlayerController player)
    {
        if (player.scoreBoosted) return;

        earlierScore = player.scoreMultiplier;
        player.scoreMultiplier *= scoreMultiplier;

        meshRenderer.enabled = false;
        objectCollider.enabled = false;
        player.scoreBoosted = true;

        StartCoroutine(CountdownToReset(player));

    }

    public override void ResetPlayer(PlayerController player)
    {
        player.scoreMultiplier = earlierScore;
        player.scoreBoosted = false;
    }
}
