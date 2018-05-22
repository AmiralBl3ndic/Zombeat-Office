using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixerMaster;
    public AudioMixer AudioMixerMusic;
    public AudioMixer AudioMixerSfx;
    public AudioMixer AudioMixerBeats;
    

    public void SetVolumeMaster( float volume )
    {
        AudioMixerMaster.SetFloat("VolumeMaster", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        AudioMixerMusic.SetFloat("VolumeMusic", volume);
    }

    public void SetVolumeSfx(float volume)
    {
        AudioMixerSfx.SetFloat("VolumeSfx", volume);
    }

    public void SetVolumeBeats(float volume)
    {
        AudioMixerBeats.SetFloat("VolumeBeats", volume);
    }


    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
