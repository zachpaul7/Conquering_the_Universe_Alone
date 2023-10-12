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
    public bool isBGMPlay;
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource[] sfxPlayer = null;
    [SerializeField] Sound[] sfx = null;

    /*    [SerializeField] Slider masterVolumeSlider; // ������ ���� ���� �����̴�
        [SerializeField] Text masterVolumeText; // ������ ���� ���� ǥ���� �ؽ�Ʈ �ʵ�

        [SerializeField] Slider bgmVolumeSlider; // BGM ���� ���� �����̴�
        [SerializeField] Text bgmVolumeText; // BGM ���� ���� ǥ���� �ؽ�Ʈ �ʵ�

        [SerializeField] Slider sfxVolumeSlider; // SFX ���� ���� �����̴�
        [SerializeField] Text sfxVolumeText; // SFX ���� ���� ǥ���� �ؽ�Ʈ �ʵ�*/

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        /*        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
                bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
                sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

                // �ʱ� ���� ����
                SetMasterVolume(masterVolumeSlider.value);
                SetBGMVolume(bgmVolumeSlider.value);
                SetSFXVolume(sfxVolumeSlider.value);*/
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        // ���� ���� �ؽ�Ʈ�� ǥ��
        /*        masterVolumeText.text = "������ ����: " + (volume * 100).ToString("F0") + "%";*/
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = volume;
        // ���� ���� �ؽ�Ʈ�� ǥ��
        /*        bgmVolumeText.text = "BGM ����: " + (volume * 100).ToString("F0") + "%";*/
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxPlayer)
        {
            sfxSource.volume = volume;
        }
        // ���� ���� �ؽ�Ʈ�� ǥ��
        /*        sfxVolumeText.text = "SFX ����: " + (volume * 100).ToString("F0") + "%";*/
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void PlayBGM(string p_bgmName)
    {
        isBGMPlay = true;
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
        isBGMPlay = false;
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
                    // SFXPlayer���� ��� ������ ���� Audio Source�� �߰��ߴٸ� 
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }

                return;
            }
        }
        Debug.Log(p_sfxName + " �̸��� ȿ������ �����ϴ�.");
        return;
    }
}