using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomicSpawn : MonoBehaviour
{
    public GameObject pfb_cacador;
    public GameObject pfb_papeis;
    public float areaX, areaZ;
    [SerializeField] private bool canRespawnHunter;
    [SerializeField] private bool canRespawnPaper;

    void Start()
    {
        int qtdCacadores = 0, qtdPapel = 0;

        int dificuldade = PlayerPrefs.GetInt("NivelDeDificuldade", 0);

        if(dificuldade <= 2) 
        {
            qtdCacadores = 5;
            qtdPapel = 5;

            if(dificuldade == 2) qtdPapel = 10;
        } 
        else {
            qtdCacadores = 10;
            qtdPapel = 20;
        }
        
        if(canRespawnHunter)
        {
            for (int i = 0; i < qtdCacadores; i++)
            {
                float posX = Random.Range(-areaX/2, areaX/2);
                float posZ = Random.Range(-areaZ/2, areaZ/2);
                Vector3 posicaoCacador = transform.position + new Vector3(posX, 0f, posZ);

                GameObject novoCacador = Instantiate(pfb_cacador, posicaoCacador, Quaternion.Euler(0f, 0f, 0f));
            }
        }

        if(canRespawnPaper)
        {
            for(int i = 0; i < qtdPapel; i++)
            {
                float posX = Random.Range(-areaX/2, areaX/2);
                float posZ = Random.Range(-areaZ/2, areaZ/2);
                Vector3 posicaoPapel = transform.position + new Vector3(posX, 0.5f, posZ);

                GameObject novoPapel = Instantiate(pfb_papeis, posicaoPapel, Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }
}
