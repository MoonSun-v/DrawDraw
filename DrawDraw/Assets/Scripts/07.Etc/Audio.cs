using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum EAudioMixerType { BGM, SFX }
public class Audio : MonoBehaviour
{
    public static Audio Instance;
    [SerializeField] private AudioMixer audioMixer;

    private float[] audioVolumes = new float[3];
    private void Awake()
    {
        Instance = this;
    }

    public void SetAudioVolume(EAudioMixerType audioMixerType, float volume)
    {
        // 오디오 믹서의 값은 -80 ~ 0까지이기 때문에 0.0001 ~ 1의 Log10 * 20을 한다.
        audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
    }

    public void SetAudioMute(EAudioMixerType audioMixerType)
    {
        int type = (int)audioMixerType;
        SetAudioVolume(audioMixerType, audioVolumes[type]);
    }


    public void ChangeBGMVolume(float volume)
    {
        Audio.Instance.SetAudioVolume(EAudioMixerType.BGM, volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        Audio.Instance.SetAudioVolume(EAudioMixerType.SFX, volume);
    }
}