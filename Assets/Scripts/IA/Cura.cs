using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cura : StateMachineBehaviour
{
    GameObject[] curas;
    GameObject cura;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MeshRenderer>().material.color = Color.white;                                                                        //cambia el color del agente al entrar en el estado

        curas = GameObject.FindGameObjectsWithTag("Cura");                                                                                         //se buscan los gameobject con el tag Cura
        

        float distanciaAuxiliar = 99999999999999999;

        for (int i = 0; i < curas.Length; i++)                                                                                                      //comienza en cero, si i es menor que el número de curas se le suma uno
        {
            float distance = Vector3.Distance(animator.transform.position, curas[i].transform.position);                                            //mira la distancia entre dos objetos y devuelve la distancia

            if (distance < distanciaAuxiliar)                                                                                                       //si la distancia obtenida es menor que la distancia auxiliar
            {
                distanciaAuxiliar = distance;                                                                                                       //la variable distanciaAuxiliar obtiene el valor de la variable dist

                cura = curas[i];                                                                                                                    //se designa qué cura es
            }
        }
        animator.GetComponent<NavMeshAgent>().destination = cura.transform.position;                                                                //se le asigna al agent la posición de la cura más cercana


    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetComponent<NavMeshAgent>().remainingDistance < 0.5f)                                                                         //si la distancia entre el agente y el punto de cura es menor que 0.5
        {
            animator.SetTrigger("Cura");                                                                                                            //setea el trigger a true
        }

    }
}
