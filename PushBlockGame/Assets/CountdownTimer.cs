using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    public static event Action FinishTimer;

    public float timeRemaining = 60; // Geri say�m s�resi (saniye olarak)
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        LevelController.MissionDone += () => { timerIsRunning = false; };
        // Geri say�m� ba�lat
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                FinishTimer?.Invoke();
                Debug.Log("Zaman doldu!");
                timeRemaining = 0;
                timerIsRunning = false;
                TimerEnded();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1; // Yava� kapanmay� engellemek i�in

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        // Buraya zaman doldu�unda yap�lacak i�lemleri ekleyin
        // �rne�in: Binay� kurtaramad�n�z mesaj� g�sterme
        Debug.Log("Malesef... Binay� zaman�nda g��lendiremediniz ve bina y�k�ld�.");
    }
}
