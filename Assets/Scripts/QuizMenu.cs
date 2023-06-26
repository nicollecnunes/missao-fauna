using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quiz;
using TMPro;

public class QuizMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject quizMenuUI;
    public TMP_Text enunciado, alt1, alt2, alt3;

    public List<Pergunta> createData()
    {
        List<Pergunta> perguntas = new List<Pergunta>();
        perguntas.Add(new Pergunta("enunciado 1", "resposta 1.1", "resposta 1.2","resposta 1.3", 1));
        perguntas.Add(new Pergunta("enunciado 2", "resposta 2.1", "resposta 2.2","resposta 2.3", 2));
        return perguntas;
    }

    public Pergunta selectQuestion(List<Pergunta> lista)
    {
        int index = Random.Range(0, lista.Count);
        return lista[index];
    }

    public void setTexts(Pergunta pgt)
    {
        enunciado.SetText(pgt.enunciado);
        alt1.SetText(pgt.alt1);
        alt2.SetText(pgt.alt2);
        alt3.SetText(pgt.alt3);
    }

    public void Start()
    {
        quizMenuUI.SetActive(false);
        List<Pergunta> listaPerguntas = createData();
        setTexts(selectQuestion(listaPerguntas));
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        quizMenuUI.SetActive(false);
    }
}
