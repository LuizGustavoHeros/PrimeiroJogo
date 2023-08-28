using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companheiro : MonoBehaviour
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


        if(distancia > 1.7)
        {
            Vector3 direcao = Jogador.transform.position - transform.position;

            GetComponent<Rigidbody>().MovePosition(
                GetComponent<Rigidbody>().position +
                (direcao.normalized * Velocidade * Time.deltaTime));

            GetComponent<Animator>().SetBool("Ataque", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Ataque", true);
        }
        
    }
    void Socao()
        {
            PlayerController PlayerScript = Jogador.GetComponent<PlayerController>();
            PlayerScript.TextoGameOver.SetActive(true);
            Time.timeScale = 0;
            PlayerScript.Vivo = false;
        }
}
