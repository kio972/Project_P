using JinWon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum BGM_Type
{
    BGM_Title = 0,
    BGM_Calendar = 1,
    BGM_Town = 2,
    BGM_Guild = 3,
    BGM_Battle = 4,
}

public enum SFX_Type
{
    Warrior_BasicAttack = 0,
    Smash_TakeDamage = 1,
    Warrior_Dash = 2,
    Warrior_Jump = 3,
    PotalWarp = 4,
    Spear_Day_Sting = 5,
    Spear_Sting_TakeDamage = 6,
    Click_on = 7,
    Click_off = 8,
}

public enum Sound_Type
{
    BGM,
    SFX,
    Master,
}

public class SoundManager : Singleton<SoundManager>
{
    private new void Awake()
    {
        base.Awake();
    }

    [SerializeField]
    private AudioSource bgmAudio;

    [SerializeField]
    private List<AudioClip> bgmList;

    [SerializeField]
    private AudioMixer audioMixer;

    public void ChangeBGM(BGM_Type BGM)
    {
        StartCoroutine(ChangeBGMClip(bgmList[(int)BGM]));
    }

    float current;
    float percent;

    IEnumerator ChangeBGMClip(AudioClip audioClip)
    {
        current = 0;
        percent = 0;
        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmAudio.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        bgmAudio.clip = audioClip;
        bgmAudio.Play();
        current = 0;
        percent = 0;
        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmAudio.volume = Mathf.Lerp(0f, GameManager.Inst.PlayerInfo.bgm, percent);
            yield return null;
        }
    }

    private int cursur = 0;

    [SerializeField]
    private List<AudioSource> sfxPlayers;

    [SerializeField]
    private List<AudioClip> sfxList;

    private int sfxIndex;

    public void PlaySFX(string SFX)
    {
        sfxIndex = GameManager.Inst.PlayerInfo.sfxList.FindIndex(x => x == SFX);
        sfxPlayers[cursur].clip = sfxList[sfxIndex];
        sfxPlayers[cursur].Play();

        cursur++;
        if (cursur > 9)
            cursur = 0;
    }

    public void BGM_Play()
    {
        bgmAudio.Play();
    }

    public void BGM_Stop()
    {
        bgmAudio.Stop();
    }

    public void PlayerRun(string SFX)
    {
        sfxIndex = GameManager.Inst.PlayerInfo.sfxList.FindIndex(x => x == SFX);
        sfxPlayers[cursur].clip = sfxList[sfxIndex];
        if (!sfxPlayers[cursur].isPlaying)
        {
            sfxPlayers[cursur].Play();
        }
    }
}
