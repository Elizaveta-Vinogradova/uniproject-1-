using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [Header("Music")]
    public AudioSource musicSource;
    public Slider musicSlider;

    [Header("SFX")]
    public AudioSource sfxSource;
    public Slider sfxSlider;

    private void Awake()
    {
        instance = this;
        var savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        var savedSfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);

        musicSource.volume = savedMusicVolume;
        sfxSource.volume = savedSfxVolume;

        if (musicSlider != null)
            musicSlider.value = savedMusicVolume;

        if (sfxSlider != null)
            sfxSlider.value = savedSfxVolume;
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void ChangeSfxVolume()
    {
        sfxSource.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
        PlayerPrefs.Save();
    
        var traps = FindObjectsOfType<TrapSound>();
        foreach (var trap in traps)
        {
            if (trap.audioSource != null)
                trap.audioSource.volume = sfxSlider.value;
        }
    }

    public void PlaySound(AudioClip sound) => sfxSource.PlayOneShot(sound);
}