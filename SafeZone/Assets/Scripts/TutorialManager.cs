using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialScreen;

    private void Start()
    {
        //PlayerPrefs.SetInt("Tutorial", 0);
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            //Stop the game
            Time.timeScale = 0;
            //Show tutorial
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        tutorialScreen.SetActive(true);
    }
    public void StartTheGame()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        tutorialScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
