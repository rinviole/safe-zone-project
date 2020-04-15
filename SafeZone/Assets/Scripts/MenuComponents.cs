using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuComponents : MonoBehaviour
{
    //Difficulty Panel
    public Animator difficultyScreenAnimator;
    //StartButton
    public GameObject startButton;
    Image startButtonImage;
    public float startButtonAlpha;

    //HighScore
    [SerializeField] private Animator highScoreAnimator;
    [SerializeField] private GameObject highScoreIcon;
    [SerializeField] GameObject highScorePanel;
    [SerializeField] private Text highScoreText;
    

    private void Start()
    {
        //Sound Prefs
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.GetInt("Sound");
        }
        //Tutorial Prefs
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        else
        {
            PlayerPrefs.GetInt("Tutorial");
        }

        //Destroy All Prefs
        if (PlayerPrefs.HasKey("DestroyAll"))
        {
            //Debug.Log($"Amount: {PlayerPrefs.GetInt("DestroyAll")}");
            PlayerPrefs.GetInt("DestroyAll");
        }
        else
        {
            PlayerPrefs.SetInt("DestroyAll", 1);
        }
        
        startButtonImage = startButton.GetComponent<Image>();
    }
   
    public void OnStartButtonDown()
    {
        highScoreIcon.SetActive(false);
        startButtonImage.color = new Color(startButtonImage.color.r, startButtonImage.color.g, startButtonImage.color.b, startButtonAlpha);
        difficultyScreenAnimator.SetBool("showDifficultyScreen", true);
    }
    
    public void HighScoreButton(bool isActive)
    {
        startButton.SetActive(!isActive);
        highScoreIcon.SetActive(!isActive);
        highScorePanel.SetActive(isActive);
        highScoreAnimator.SetBool("highScorePanel", isActive);
        int highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
    }
    
}
