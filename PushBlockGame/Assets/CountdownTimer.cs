using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    public static event Action FinishTimer;

    public float timeRemaining = 60; // Geri sayým süresi (saniye olarak)
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        LevelController.MissionDone += () => { timerIsRunning = false; };
        // Geri sayýmý baþlat
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
        timeToDisplay += 1; // Yavaþ kapanmayý engellemek için

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        // Buraya zaman dolduðunda yapýlacak iþlemleri ekleyin
        // Örneðin: Binayý kurtaramadýnýz mesajý gösterme
        Debug.Log("Malesef... Binayý zamanýnda güçlendiremediniz ve bina yýkýldý.");
    }
}
