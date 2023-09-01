using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private PlayerController scriptPlayerController;
    public Slider SliderVidaJogador;

    void Start()
    {
        scriptPlayerController = GameObject.FindWithTag("Jogador").GetComponent<PlayerController>();
        SliderVidaJogador.maxValue = scriptPlayerController.Vida;
        AtualizaSlideVidaJogador();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizaSlideVidaJogador ()
    {
        SliderVidaJogador.value = scriptPlayerController.Vida;
    }
}
