using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private Slider _slider;
    private void Start()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/GeneralAudio");
        _slider = GetComponent<Slider>();
        _slider.value = 4;
    }

    public void MusicVolume(Slider slider)
    {
        switch (slider.value)
        {
            case 0:
                audioMixer.SetFloat("Music", -88f);
                break;
            case 1:
                audioMixer.SetFloat("Music", -40f);
                break;
            case 2:
                audioMixer.SetFloat("Music", -20f);
                break;
            case 3:
                audioMixer.SetFloat("Music", -10f);
                break;
            case 4:
                audioMixer.SetFloat("Music", 0f);
                break;
            case 5:
                audioMixer.SetFloat("Music", 10f);
                break;
            default:

                break;
        }
    }

    public void AudioVolumeSelect(Slider slider)
    {
        switch (slider.value)
        {

            case 0:
                audioMixer.SetFloat("SoundEffect", -88f);
                break;
            case 1:
                audioMixer.SetFloat("SoundEffect", -40f);
                break;
            case 2:
                audioMixer.SetFloat("SoundEffect", -20f);
                break;
            case 3:
                audioMixer.SetFloat("SoundEffect", -10f);
                break;
            case 4:
                audioMixer.SetFloat("SoundEffect", 0f);
                break;
            case 5:
                audioMixer.SetFloat("SoundEffect", 10f);
                break;
            default:
                break;
        }
    }
}
