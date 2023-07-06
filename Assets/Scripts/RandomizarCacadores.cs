using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizarCacadores : MonoBehaviour
{
    public GameObject pfb_cacador;
    public GameObject pfb_papeis;
    public int minCacadorPapel, maxCacadorPapel;
    public float areaX, areaZ;

    void Start()
    {
        int qtdCacadores = Random.Range(minCacadorPapel, maxCacadorPapel + 1);
        int qtdPapel = qtdCacadores;
        for (int i = 0; i < qtdCacadores; i++)
        {
            float posX = Random.Range(-areaX/2, areaX/2);
            float posZ = Random.Range(-areaZ/2, areaZ/2);
            Vector3 posicaoCacador = transform.position + new Vector3(posX, 0f, posZ);
            //todo rodar cacadores
            GameObject novoCacador = Instantiate(pfb_cacador, posicaoCacador, Quaternion.Euler(0f, 0f, 0f));

            posX = Random.Range(-areaX/2, areaX/2);
            posZ = Random.Range(-areaZ/2, areaZ/2);
            Vector3 posicaoPapel = transform.position + new Vector3(posX, 0f, posZ);
            //todo rodar cacadores
            GameObject novoPapel = Instantiate(pfb_papeis, posicaoPapel, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
