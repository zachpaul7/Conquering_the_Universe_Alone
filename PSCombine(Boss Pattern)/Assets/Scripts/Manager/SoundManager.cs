using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource[] sfxPlayer = null;
    [SerializeField] Sound[] sfx = null;

    [SerializeField] Slider masterVolumeSlider; // 마스터 볼륨 조절 슬라이더
    [SerializeField] Text masterVolumeText; // 마스터 볼륨 값을 표시할 텍스트 필드

    [SerializeField] Slider bgmVolumeSlider; // BGM 볼륨 조절 슬라이더
    [SerializeField] Text bgmVolumeText; // BGM 볼륨 값을 표시할 텍스트 필드

    [SerializeField] Slider sfxVolumeSlider; // SFX 볼륨 조절 슬라이더
    [SerializeField] Text sfxVolumeText; // SFX 볼륨 값을 표시할 텍스트 필드

    private void Start()
    {
        // 슬라이더 초기값 설정
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // 초기 볼륨 적용
        SetMasterVolume(masterVolumeSlider.value);
        SetBGMVolume(bgmVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        // 볼륨 값을 텍스트로 표시
        masterVolumeText.text = "마스터 볼륨: " + (volume * 100).ToString("F0") + "%";
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = volume;
        // 볼륨 값을 텍스트로 표시
        bgmVolumeText.text = "BGM 볼륨: " + (volume * 100).ToString("F0") + "%";
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxPlayer)
        {
            sfxSource.volume = volume;
        }
        // 볼륨 값을 텍스트로 표시
        sfxVolumeText.text = "SFX 볼륨: " + (volume * 100).ToString("F0") + "%";
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }
        Debug.Log(p_sfxName + " 이름의 효과음이 없습니다.");
        return;
    }
}