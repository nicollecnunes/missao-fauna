using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomicSpawn : MonoBehaviour
{
    public GameObject pfb_cacador;
    public GameObject pfb_papeis;
    public int minCacador, maxCacador;
    public float areaX, areaZ;
    [SerializeField] private bool canRespawnHunter;
    [SerializeField] private bool canRespawnPaper;

    void Start()
    {
        int qtdCacadores = 0, qtdPapel = 0;
        if(canRespawnHunter) qtdCacadores = Random.Range(minCacador, maxCacador + 1);
        if(canRespawnPaper) qtdPapel = qtdCacadores;
        
        for (int i = 0; i < qtdCacadores; i++)
        {
            float posX = Random.Range(-areaX/2, areaX/2);
            float posZ = Random.Range(-areaZ/2, areaZ/2);
            Vector3 posicaoCacador = transform.position + new Vector3(posX, 0f, posZ);

            GameObject novoCacador = Instantiate(pfb_cacador, posicaoCacador, Quaternion.Euler(0f, 0f, 0f));

            posX = Random.Range(-areaX/2, areaX/2);
            posZ = Random.Range(-areaZ/2, areaZ/2);
            Vector3 posicaoPapel = transform.position + new Vector3(posX, 0.5f, posZ);

            GameObject novoPapel = Instantiate(pfb_papeis, posicaoPapel, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
