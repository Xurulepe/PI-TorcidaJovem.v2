using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("UI Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private const string MASTER_VOLUME_STRING = "MasterVolume";
    private const string MUSIC_VOLUME_STRING = "MusicVolume";
    private const string SFX_VOLUME_STRING = "SFXVolume";

    #region SINGLETON
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region PLAY SOUNDS
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }
    #endregion

    #region SET VOLUMES

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MASTER_VOLUME_STRING, Mathf.Log10(volume) * 20);
        masterVolumeSlider.value = volume;

        PlayerPrefs.SetFloat(MASTER_VOLUME_STRING, volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_STRING, Mathf.Log10(volume) * 20);
        musicVolumeSlider.value = volume;

        PlayerPrefs.SetFloat(MUSIC_VOLUME_STRING, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFX_VOLUME_STRING, Mathf.Log10(volume) * 20);
        sfxVolumeSlider.value = volume;

        PlayerPrefs.SetFloat(SFX_VOLUME_STRING, volume);
    }
    #endregion

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MASTER_VOLUME_STRING))
        {
            SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_STRING));
        }
        if (PlayerPrefs.HasKey(MUSIC_VOLUME_STRING))
        {
            SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_STRING));
        }
        if (PlayerPrefs.HasKey(SFX_VOLUME_STRING))
        {
            SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_STRING));
        }
    }

    public void SetSliders(AudioHelper helper)
    {
        masterVolumeSlider = helper.masterSlider;
        musicVolumeSlider = helper.musicSlider;
        sfxVolumeSlider = helper.sfxSlider;
    }
}
