using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float Velocidade = 8;
    Vector3 direcao;     
    public LayerMask MascaraChao;

    public GameObject TextoGameOver;
    public bool Vivo = true;

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if(direcao != Vector3.zero){
            GetComponent<Animator>().SetBool("Run", true);
        }
        else{
             GetComponent<Animator>().SetBool("Run", false);
        }
        if(Vivo == false)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }

        
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition
        (GetComponent<Rigidbody>().position + 
        (direcao * Time.deltaTime * Velocidade));
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;
        if(Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }

    }
}