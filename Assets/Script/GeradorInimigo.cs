using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigo : MonoBehaviour
{
    public GameObject Inimigo;
    private float contadorTempo = 0;
    public float TempoGerarInimigo = 1;
    
 
    void Update()
    {
        contadorTempo += Time.deltaTime;
        if(contadorTempo >= TempoGerarInimigo)
        {
            Instantiate(Inimigo, transform.position, transform.rotation);
            contadorTempo = 0;
        }
        
    }
}