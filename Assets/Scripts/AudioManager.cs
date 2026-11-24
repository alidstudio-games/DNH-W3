using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private GameObject offSymbol;
    private bool soundOn;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundOn = true;

        offSymbol.SetActive(false);
    }

    public void SoundOnOffButton()
    {
        if (soundOn)
        {
            PlayButtonClick();
            soundOn = false;

            offSymbol.SetActive(true);
            audioSource.volume = 0f;
        }
        else
        {
            PlayButtonClick();
            soundOn = true;

            offSymbol.SetActive(false);
            audioSource.volume = 1f;
        }
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }
}
