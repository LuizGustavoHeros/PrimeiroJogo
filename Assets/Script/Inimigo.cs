using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public GameObject Jogador;
    public float Velocidade = 5;

    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        Vector3 direcaoJogador = Jogador.transform.position - transform.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcaoJogador);
        GetComponent<Rigidbody>().MoveRotation(novaRotacao);

        if(distancia < 1.7)
        {
             GetComponent<Animator>().SetBool("Ataque", true);
        }
        else
        {
           Vector3 direcao = Jogador.transform.position - transform.position;

            GetComponent<Rigidbody>().MovePosition(
                GetComponent<Rigidbody>().position +
                (direcao.normalized * Velocidade * Time.deltaTime));

            GetComponent<Animator>().SetBool("Ataque", false);
        }
        
        // if(distancia > 1.7)
        // {
        //     Vector3 direcao = Jogador.transform.position - transform.position;

        //     GetComponent<Rigidbody>().MovePosition(
        //         GetComponent<Rigidbody>().position +
        //         (direcao.normalized * Velocidade * Time.deltaTime));

        //     GetComponent<Animator>().SetBool("Ataque", false);
        // }
        // else
        // {
        //     GetComponent<Animator>().SetBool("Ataque", true);
        // }
        
    }
    void Socao()
        {
            int dano = Random.Range(1, 3);
            PlayerController PlayerScript = Jogador.GetComponent<PlayerController>();
            PlayerScript.TomarDano(dano);
            
        }
}
