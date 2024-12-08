using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _sfxSlider.value = AudioManager.Instance.GetSFXVolume();
    }
}
