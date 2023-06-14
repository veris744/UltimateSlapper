using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class SlapCounter : MonoBehaviour
{
    private int pointsToAdd = 100;
    private void OnCollisionEnter(Collision collision)
    {
        SlapCounter slapCount = collision.gameObject.GetComponent<SlapCounter>();
        if (slapCount)
        {
            slapCount.OnCollisionWithSlapped();
        }
    }

    public void OnCollisionWithSlapped()
    {
        ((GameManager)GameManager.Instance).slapCount++;
        ((GameManager)GameManager.Instance).comboTimer = ConstParamenters.COMBO_TIMER_DEFAULT;

        Debug.Log(((GameManager)GameManager.Instance).slapCount);
    }
}
