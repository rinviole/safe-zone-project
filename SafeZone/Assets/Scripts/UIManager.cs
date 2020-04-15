using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    //GameOverScreen
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private GameObject rewardScreen;
    [SerializeField] private AudioClip rewardSound;
    //PauseScreen
    [SerializeField] private GameObject pauseScreen;
    //Score
    [SerializeField] Text scoreText;
    private int score;
    public Text highScoreText;
    public Animator uiAnimator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        score = 0;
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        HighScore();
    }

    void HighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }
    }

    public void UpdateScore(int _score, bool scoreIncrease)
    {
        if (!GameOver.instance.GameIsOver)
        {
            score += _score;
            scoreText.text = $"SCORE: {score.ToString()}";
            uiAnimator.SetBool("scoreIncrease", scoreIncrease);
        }
    }

    public void PlayRewardAnimation(bool value)
    {
        if (value == true)
        {
            AudioSource.PlayClipAtPoint(rewardSound, Camera.main.transform.position);
        }
        rewardScreen.SetActive(value);
        uiAnimator.SetBool("giveReward", value);
    }

    public void PauseScreen(bool pauseIsActive)
    {
        if (!GameOver.instance.GameIsOver)
        {
            if (pauseIsActive == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseScreen.SetActive(pauseIsActive);
        }
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
