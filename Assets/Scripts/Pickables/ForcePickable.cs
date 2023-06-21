using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePickable : Pickable
{
    public float forceMultiplier = 2;
    private float earlierForce;

    
    public override void OnTriggerWithPlayer(PlayerController player)
    {
        if (player.forceBoosted) return;

        earlierForce = player.playerForce;
        player.playerForce *= forceMultiplier;

        meshRenderer.enabled = false;
        objectCollider.enabled = false;
        player.forceBoosted = true;

        StartCoroutine(CountdownToReset(player));

    }

    public override void ResetPlayer(PlayerController player)
    {
        player.playerForce = earlierForce;
        player.forceBoosted = false;
    }

    
}
