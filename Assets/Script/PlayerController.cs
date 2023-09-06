using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Velocidade = 10;
    Vector3 direcao;
    public LayerMask MascaraChao;

    public GameObject TextoGameOver;
    public GameObject GameOver;

    public int Vida = 10;
    public ControlaInterface scriptControlaInterface;

    public AudioClip SomDeDano;
    public AudioClip SomDeMorte;

    private bool estaNoChao = true;
    private Rigidbody rb;

    public float alturaDoSalto = 10f; // Variável pública para a altura do salto.

    public float velocidadeDeRotacao = 5f; // Ajuste a velocidade de rotação aqui.

    void Start()
    {
        Time.timeScale = 1;
        TextoGameOver.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Lógica de rotação do personagem
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = (impacto.point - transform.position).normalized;
            posicaoMiraJogador.y = 0; // Zere a componente Y para evitar inclinação para baixo.
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, novaRotacao, Time.deltaTime * velocidadeDeRotacao));
        }

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

        if (estaNoChao && Input.GetButtonDown("Jump"))
        {
            Salto();
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
        // Movimento do personagem
        rb.velocity = new Vector3(direcao.x * Velocidade, rb.velocity.y, direcao.z * Velocidade);
    }

    public void TomarDano(int dano)
    {
        Vida -= dano;
        scriptControlaInterface.AtualizaSlideVidaJogador();
        ControlaAudio.instance.PlayOneShot(SomDeDano);
        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
            GameOver.SetActive(true);
            ControlaAudio.instance.PlayOneShot(SomDeMorte);
        }
    }

    void Salto()
    {
        GetComponent<Animator>().SetTrigger("Jump");
        rb.AddForce(Vector3.up * alturaDoSalto, ForceMode.Impulse); // Use a variável pública para a altura do salto.
        estaNoChao = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PlayerController : MonoBehaviour
// {

//     public float Velocidade = 10;
//     // CharacterController characterController;
//     Vector3 direcao;
//     public LayerMask MascaraChao;

//     public GameObject TextoGameOver;
//     public GameObject GameOver;

//     public int Vida = 10;
//     public ControlaInterface scriptControlaInterface;

//     public AudioClip SomDeDano; // Audio de dano
//     public AudioClip SomDeMorte;

//     void Start()
//     {
//         Time.timeScale = 1;
//         TextoGameOver.SetActive(false);
//         // characterController = GetComponent<CharacterController>();
//     }

//     void Update()
//     {
//         float eixoX = Input.GetAxis("Horizontal");
//         float eixoZ = Input.GetAxis("Vertical");

//         direcao = new Vector3(eixoX, 0, eixoZ);

//         if (direcao != Vector3.zero)
//         {
//             GetComponent<Animator>().SetBool("Run", true);
//         }
//         else
//         {
//             GetComponent<Animator>().SetBool("Run", false);
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
//         GetComponent<Rigidbody>().MovePosition
//         (GetComponent<Rigidbody>().position + 
//         (direcao * Time.deltaTime * Velocidade));
//         Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
//         Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

//         RaycastHit impacto;
//         if(Physics.Raycast(raio, out impacto, 100, MascaraChao))
//         {
//             Vector3 posicaoMiraJogador = impacto.point - transform.position;
//             Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
//             GetComponent<Rigidbody>().MoveRotation(novaRotacao);
//         }

//     }
//     public void TomarDano (int dano){
//         Vida -= dano;
//         scriptControlaInterface.AtualizaSlideVidaJogador();
//         ControlaAudio.instance.PlayOneShot(SomDeDano); // Toca o som de dano
//         if(Vida <= 0)
//         {
//             Time.timeScale = 0;
//             TextoGameOver.SetActive(true);
//             GameOver.SetActive(true);
//             ControlaAudio.instance.PlayOneShot(SomDeMorte);
//         }
//     }
// }