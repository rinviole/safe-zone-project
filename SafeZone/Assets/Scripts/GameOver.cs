using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    private bool gameIsOver;
    public bool GameIsOver { get { return gameIsOver; } set { gameIsOver = value; } }

    private void Start()
    {
        instance = this;
        FindObjectOfType<GameManager>().OnGameOverEvent += OnGameOver;
    }

    void OnGameOver()
    {
        gameIsOver = true;
        UIManager.instance.ShowGameOverScreen();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        if (gameIsOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
