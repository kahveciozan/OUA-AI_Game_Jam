using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject congratulationsPanel;
    public GameObject failedPanel;


    void Start()
    {
        fadePanel.SetActive(false);
        EarthquakeEffect.AfterEartquakeEffect += OpenFailedPanel;
        LevelController.AfterConfettiEffect += OpenCongratulationsPanel;
    }


    private void OpenCongratulationsPanel()
    {
        congratulationsPanel.SetActive(true);
        Debug.Log("Congratulations!");
    }

    public void OpenFailedPanel()
    {
        failedPanel.SetActive(true);
        Debug.Log("Failed!");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        fadePanel.SetActive(false);
        congratulationsPanel.SetActive(false);
        failedPanel.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





}
