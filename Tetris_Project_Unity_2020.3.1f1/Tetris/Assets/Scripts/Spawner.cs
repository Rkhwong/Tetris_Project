using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Recebe quais GameObjects vai Spawnar, colocados em uma Lista
    public GameObject[] groups;

    private void Start()
    {
        spawnNext();
    }

    public void spawnNext()
    {
        //Recebe um Objeto Randomico da Lista Criada
        int randomNum = Random.Range(0, groups.Length);

        //Spawna um GameObject no local
        //transform.postion recebe a posicao do GameObject>Spawner, Quaternion.identiy é o Rotation Default, pode ser usado transform.rotation
        Instantiate(groups[randomNum], transform.position, Quaternion.identity);

    }
}

