using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthAndPointsController : MonoBehaviour
{
    // Health
    public int health = 3;
    public int numOfHearts = 3;

    public Image[] hearts;
    public Sprite life;
    public Sprite lostLife;

    // Points
    public int points;
    public int pointsToConvert = 1;

    public TMP_Text text;

    private void Start()
    {
        text.text = "0";
        int dificuldade = PlayerPrefs.GetInt("NivelDeDificuldade", 0);
        if(dificuldade >= 2) pointsToConvert = 2;
    }

    private void Update()
    {
        if(health > numOfHearts) health = numOfHearts;
        else if(health < 0) health = 0;

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health) hearts[i].sprite = life;
            else hearts[i].sprite = lostLife;

            if(i < numOfHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void AddPoint(int pointsToAdd)
    {
        int currentValue = Int32.Parse(text.text);
        Debug.Log(currentValue);
        Debug.Log(pointsToAdd);
        int newValue = currentValue + pointsToAdd;
        Debug.Log(newValue);

        points = newValue;
        text.text = newValue.ToString();
    }

    public void ConvertHunter()
    {
        int currentValue = Int32.Parse(text.text);
        int newValue = currentValue - pointsToConvert;

        if(newValue < 0) newValue = 0;

        points = newValue;
        text.text = newValue.ToString();
    }
}
