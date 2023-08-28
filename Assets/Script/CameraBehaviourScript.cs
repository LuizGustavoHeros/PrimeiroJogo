using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    public GameObject player;
    Vector3 distanciaCameraPlayer;

    void Start()
    {
        distanciaCameraPlayer = transform.position - player.transform.position;
    }


    void Update()
    {
        transform.position = player.transform.position + distanciaCameraPlayer;
    }
}
