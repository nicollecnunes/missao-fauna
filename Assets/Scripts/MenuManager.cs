using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeCenaInicial;
    // [SerializeField] private GameObject painelMenuInicial;
    // [SerializeField] private GameObject painelOpcoes;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeCenaInicial);
    }
    public void AbrirOpcoes()
    {
        SceneManager.LoadScene(nomeCenaInicial);
    }
    public void Sair()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit(); //fecha o jogo
    }
}
