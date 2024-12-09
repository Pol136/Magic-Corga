using System.IO;
using UnityEngine;

public class SaveMusicSettings : MonoBehaviour
{
    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = AudioManager.Instance;
        LoadData();
    }

    public void SaveData()
    {
        MusicSettings musicSettings = new MusicSettings(_audioManager.GetMusicVolume(), _audioManager.GetSFXVolume());

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "music_settings.json"))
        {
            writer.Write(JsonUtility.ToJson(musicSettings));
        }
    }

    public void LoadData()
    {
        MusicSettings data = new MusicSettings(0, 0);

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "music_settings.json"))
        {
            data = JsonUtility.FromJson<MusicSettings>(reader.ReadToEnd());
        }

        _audioManager.MusicVolume(data.musicValue);
        _audioManager.SFXVolume(data.sfxValue);

        //if (SliderController.Instance != null)
        //{
        //    SliderController.Instance.CheckSliderValue();
        //}
        //else
        //{
        //    Debug.Log("Cant do slider control");
        //}
    }

}
