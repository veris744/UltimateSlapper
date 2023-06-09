﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guardia : StateMachineBehaviour
{
    Vector3 AddVecRight = new Vector3(1, 0, 1);
    Vector3 AddVecLeft = new Vector3(-1, 0, 1);
    Vector3 AddSpawnPosVec = new Vector3(0, 1, 0);

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<MeshRenderer>().material.color = Color.yellow;                                                                      //cambia el color del agente al entrar en el estado

        animator.GetComponent<NavMeshAgent>().destination = 
            GameObject.FindGameObjectWithTag("waypointGuardia").transform.Find("Waypoint " + animator.gameObject.name).transform.position;        //se coge el padre de los waypoints para buscarlos
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);                                                                //gira sobre el eje vertical con velocidad 100 respecto al mundo

        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.forward), out hit, 10))
        {
            if (hit.transform.tag == "Player")                                                                                                    //Si el ray encuentra el gameobject con el tag Player pasa al estado detección
            {
                animator.GetBehaviour<GoToPlayer>().playerPositionOnHit = hit.point;                                                              //se pasa la posición actual del Player al script GoToPlayer
                animator.SetBool("Player", true);                                                                                                 //se pone la condición para que el agente cambie al estado de detección
                Debug.Log("Hit player");
            }
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);     //si el ray se choca con algo cambia a color amarillo

        }
        else
        {
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.forward) * 5, Color.white);                 //Si no encuentra nada permanece en blanco
        }

        if (Physics.Raycast(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.right + AddVecRight), out hit, 5))
        {
            if (hit.transform.tag == "Player")                                                                                                    //Si el ray encuentra el gameobject con el tag Player pasa al estado detección
            {
                animator.GetBehaviour<GoToPlayer>().playerPositionOnHit = hit.point;                                                              //se pasa la posición actual del Player al script GoToPlayer
                animator.SetBool("Player", true);                                                                                                 //se pone la condición para que el agente cambie al estado de detección
                Debug.Log("Hit player");
            }
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.right + AddVecRight) * hit.distance, Color.yellow);      //si el ray se choca con algo cambia a color amarillo

        }
        else
        {
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.right + AddVecRight) * 5, Color.white);                  //Si no encuentra nada permanece en blanco
        }

        if (Physics.Raycast(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.left + AddVecLeft), out hit, 5))
        {
            if (hit.transform.tag == "Player")                                                                                                    //Si el ray encuentra el gameobject con el tag Player pasa al estado detección
            {
                animator.GetBehaviour<GoToPlayer>().playerPositionOnHit = hit.point;                                                              //se pasa la posición actual del Player al script GoToPlayer
                animator.SetBool("Player", true);                                                                                                 //se pone la condición para que el agente cambie al estado de detección
                Debug.Log("Hit player");
            }
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.left + AddVecLeft) * hit.distance, Color.yellow);      //si el ray se choca con algo cambia a color amarillo

        }
        else
        {
            Debug.DrawRay(animator.transform.position + AddSpawnPosVec, animator.transform.TransformDirection(Vector3.left + AddVecLeft) * 5, Color.white);                  //Si no encuentra nada permanece en blanco
        }
    }
   
}
