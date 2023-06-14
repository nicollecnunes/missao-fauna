using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject quizMenuUI, txtEnunciado, txtAlt1, txtAlt2, txtAlt3;

    void Start()
    {
        quizMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToGame()
    {
        quizMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
