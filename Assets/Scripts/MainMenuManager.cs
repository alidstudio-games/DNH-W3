using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private AudioSource sfxSource;
    [SerializeField] private AudioClip buttonClickClip;

    void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayButton()
    {
        PlayButtonClick();
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        PlayButtonClick();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif 
    } 

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickClip);
    }
}
