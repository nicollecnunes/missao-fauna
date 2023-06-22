using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizarCacadores : MonoBehaviour
{
    public GameObject pfb_cacador;
    public int minCacador, maxCacador;
    public float areaX, areaZ;

    void Start()
    {
        int qtdCacadores = Random.Range(minCacador, maxCacador + 1);
        for (int i = 0; i < qtdCacadores; i++)
        {
            float posX = Random.Range(-areaX/2, areaX/2);
            float posZ = Random.Range(-areaZ/2, areaZ/2);
            Vector3 posicaoCacador = transform.position + new Vector3(posX, 0f, posZ);
            //todo rodar cacadores
            Instantiate(pfb_cacador, posicaoCacador, Quaternion.Euler(-90f, 0f, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
