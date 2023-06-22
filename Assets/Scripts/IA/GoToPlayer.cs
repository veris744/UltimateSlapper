using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : StateMachineBehaviour
{
    public Vector3 playerPositionOnHit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<MeshRenderer>().material.color = Color.blue;                                                                        //cambia el color del agente al entrar en el estado

        animator.GetComponent<NavMeshAgent>().destination = playerPositionOnHit;                                                                  //se le asigna al agent la posición que se le ha pasado desde el script StatePatrol
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<NavMeshAgent>().destination = playerPositionOnHit;

        if (animator.GetComponent<NavMeshAgent>().remainingDistance < 0.5)
        {
           float distance = Vector3.Distance(animator.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distance < 3)
            {
                animator.SetBool("Attack", true);                                                                                                  /*si la distancia entre el agente y el gameobject con el tag Player es menor que 3 
                                                                                                                                                     se cambia al estado Attack*/
            }

            else animator.SetBool("Player", false);                                                                                                //si la distancia es mayor se vuelve al estado Patrol
        }
    }

}
