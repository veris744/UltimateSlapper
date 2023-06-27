using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : StateMachineBehaviour
{
    int contador;                                                                                                                                //se declara la variable contador para poder contar los ataques
    float espTime;                                                                                                                               //se declara la variable espTime para poder contar el tiempo de espera después de cada ataque
    bool hitted;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitted = false;
        //animator.GetComponent<MeshRenderer>().material.color = Color.red;                                                                        //cambia el color del agente al entrar en el estado

        contador = 0;                                                                                                                            //al empezar se pone el contador a cero
        espTime = 0;                                                                                                                             //al empezar se pone el tiempo a cero
        animator.SetBool("Player", false);                                                                                                       //si la distancia es mayor se vuelve al estado Patrol

        
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (contador >= 3 || hitted)
        {
            animator.SetBool("Attack", false);                                                                                                   //si ya ha atacado tres veces se cambia al estado Patrol
        }

        if (espTime <= 0)
        {
            Debug.Log("Attack " + (contador + 1));                                                                                               //si el tiempo es igual a cero salta el mensaje de ataque con el número del ataque correspondiente
            ++contador;                                                                                                                          //se le suma uno al contador para que en la siguiente vuelta marque el siguiente ataque
            espTime = 3;                                                                                                                         //pone el contador de tiempo a 3 segundos
        }
        else espTime -= Time.deltaTime;                                                                                                          //resta el tiempo al espTime para hacer una cuenta regresiva para lanzar el siguiente ataque

        if (!hitted)
        {
            RaycastHit hit; 
            
            if (Physics.SphereCast(animator.transform.position + new Vector3(0, 1, 0), 0.5f, animator.transform.TransformDirection(Vector3.forward), out hit, 0.75f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponentInParent<AudioSource>().Play();   
                    GameManager.Instance.AddLifes(-1);
                    hitted = true;
                }
            }
            //Debug.DrawRay(animator.transform.position + new Vector3(0, 1, 0), animator.transform.TransformDirection(Vector3.forward) * 0.5f, Color.red);

        }

        /*la condición del if se pone menor o igual que cero en vez de solo igual para evitar que por la
                                                                                                                                             resta que se hace para llegar a cero de algún tipo de error*/
    }
}
