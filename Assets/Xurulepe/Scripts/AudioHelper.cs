using UnityEngine;
using UnityEngine.UI;

public class AudioHelper : MonoBehaviour
{
    [Header("Audio Sliders")]    
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    private void Start()
    {
        AudioManager.Instance.SetSliders(this);
        AudioManager.Instance.LoadVolume();

        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
    }
}
