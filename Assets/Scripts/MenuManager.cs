using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeCenaInicial;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;

    void Start()
    {
        painelOpcoes.SetActive(false);
    }
    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaInicial);
    }
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void Sair()
    {
        Application.Quit(); //fecha o jogo
    }
}
