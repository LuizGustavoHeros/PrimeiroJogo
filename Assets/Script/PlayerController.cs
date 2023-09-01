// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PlayerController : MonoBehaviour
// {
//     public float Velocidade = 10.0f;
//     public GameObject TextoGameOver;
//     public int Vida = 100;

//     private CharacterController characterController;
//     private Animator animator;

//     void Start()
//     {
//         Time.timeScale = 1;
//         TextoGameOver.SetActive(false);
//         characterController = GetComponent<CharacterController>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         float eixoX = Input.GetAxis("Horizontal");
//         float eixoZ = Input.GetAxis("Vertical");

//         Vector3 direcao = new Vector3(eixoX, 0, eixoZ).normalized;

//         if (direcao != Vector3.zero)
//         {
//             animator.SetBool("Run", true);
//         }
//         else
//         {
//             animator.SetBool("Run", false);
//         }

//         if (Vida <= 0)
//         {
//             if (Input.GetButtonDown("Fire1"))
//             {
//                 SceneManager.LoadScene("game");
//             }
//         }
//     }

//     void FixedUpdate()
//     {
//         float eixoX = Input.GetAxis("Horizontal");
//         float eixoZ = Input.GetAxis("Vertical");

//         Vector3 direcao = new Vector3(eixoX, 0, eixoZ).normalized;
//         Vector3 move = direcao * Velocidade * Time.fixedDeltaTime;

//         characterController.Move(move);

//         Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
//         Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

//         RaycastHit impacto;
//         if (Physics.Raycast(raio, out impacto, 100))
//         {
//             Vector3 posicaoMiraJogador = impacto.point - transform.position;
//             Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
//             transform.rotation = novaRotacao;
//         }
//     }

//     public void TomarDano(int dano)
//     {
//         Vida -= dano;
//         if (Vida <= 0)
//         {
//             Time.timeScale = 0;
//             TextoGameOver.SetActive(true);
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float Velocidade = 10;
    // CharacterController characterController;
    Vector3 direcao;
    public LayerMask MascaraChao;

    public GameObject TextoGameOver;
    public int Vida = 10;
    public ControlaInterface scriptControlaInterface;

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
        // characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        if (direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", false);
        }

        if (Vida <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
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
    public void TomarDano (int dano){
        Vida -= dano;
        scriptControlaInterface.AtualizaSlideVidaJogador();
        if(Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }
}