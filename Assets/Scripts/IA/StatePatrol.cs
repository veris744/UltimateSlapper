using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class StatePatrol : StateMachineBehaviour
{
    private NavMeshAgent agent;
    List<Transform> points = new List<Transform>();
    private int destPoint = 0;
    private bool NoInicializado = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (NoInicializado)
        {
            agent = animator.GetComponent<NavMeshAgent>();                                                                                        //se declara el agente

            Transform t = GameObject.FindGameObjectWithTag("waypoints").transform.Find("Waypoints " + animator.gameObject.name).transform;        //se coge el padre de los waypoints para buscarlos

            for(int i = 0; i < t.childCount; ++i)                                                                                                 //se hace un bucle para seleccionar qué waypoints van en cada patrulla
            {
                points.Add(t.GetChild(i));
            }

            NoInicializado = false;
        }

       
        animator.GetComponent<MeshRenderer>().material.color = Color.green;                                                                       //cambia el color del agente al entrar en el estado

        GotoNextPoint();
    }
    void GotoNextPoint()                                                                                                                          //para que vaya al siguiente waypoint
    {
        if (points.Count == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Count;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position, animator.transform.TransformDirection(Vector3.forward), out hit, 5))
        {
            if (hit.transform.tag == "Player")                                                                                                    //Si el ray encuentra el gameobject con el tag Player pasa al estado detección
            {
                animator.GetBehaviour<GoToPlayer>().playerPositionOnHit = hit.point;                                                              //se pasa la posición actual del Player al script GoToPlayer
                animator.SetBool("Player", true);                                                                                                 //se pone la condición para que el agente cambie al estado de detección
                Debug.Log("Hit player");
            }
            Debug.DrawRay(animator.transform.position, animator.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);      //si el ray se choca con algo cambia a color amarillo

        }
        else
        {
            Debug.DrawRay(animator.transform.position, animator.transform.TransformDirection(Vector3.forward) * 5, Color.white);                  //Si no encuentra nada permanece en blanco
        }
    }
}
