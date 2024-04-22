using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string musicVolumeParameterName = "Music";
    public string soundVolumeParameterName = "Sound";
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle musicToggle;

    private void Start()
    {
        // Cargar configuraci�n de PlayerPrefs
        if (PlayerPrefs.HasKey("Music"))
        {
            musicToggle.isOn = PlayerPrefs.GetFloat("Music") > -1;
            SetMusicVolume();
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            bool soundVolume = PlayerPrefs.GetFloat("Sound") > -1;
            SetSoundVolume(soundVolume);
        }
    }

    public void SetMusicVolume()
    {
        var volume = musicToggle.isOn ? 0 : -80;
        audioMixer.SetFloat(musicVolumeParameterName, volume);
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetSoundVolume(bool state)
    {
        soundToggle.isOn = state;
        var volume = soundToggle.isOn ? 0 : -80;
        audioMixer.SetFloat(soundVolumeParameterName, volume);
        PlayerPrefs.SetFloat("Sound", volume);
    }
}
