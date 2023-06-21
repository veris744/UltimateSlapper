using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickable : Pickable
{
    public float speedMultiplier = 2;
    private float earlierSpeed;


    public override void OnTriggerWithPlayer(PlayerController player)
    {
        if (player.speedBoosted) return;

        earlierSpeed = player.playerSpeed;
        player.playerSpeed *= speedMultiplier;

        meshRenderer.enabled = false;
        objectCollider.enabled = false;
        player.speedBoosted = true;

        StartCoroutine(CountdownToReset(player));
    }


    public override void ResetPlayer(PlayerController player)
    {
        player.playerSpeed = earlierSpeed;
        player.speedBoosted = false;
    }

}
