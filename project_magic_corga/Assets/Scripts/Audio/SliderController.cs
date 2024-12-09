using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public static SliderController Instance;
    public Slider _musicSlider, _sfxSlider;

    public void CheckSliderValue()
    {
        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _sfxSlider.value = AudioManager.Instance.GetSFXVolume();
    }
}
