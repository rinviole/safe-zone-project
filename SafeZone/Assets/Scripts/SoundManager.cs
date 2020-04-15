using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip gameTheme;
    bool toggle = true;

    private void Start()
    {
        instance = this;
    }

    public void MuteSound(int value)
    {
        AudioListener.volume = value;
    }

    public void ToggleSound()
    {
        toggle = !toggle;
        if (toggle == true)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

}
