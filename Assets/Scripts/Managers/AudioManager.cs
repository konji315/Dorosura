using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    AudioSource _bgmAudio, _seAudio;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        //状況に応じて
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        AudioSource[] source = GetComponents<AudioSource>();

        _bgmAudio = source[0];
        _seAudio = source[1];
    }

    // Update is called once per frame
    void Update () {

    }

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="se_name">SEの名前</param>
    public void PlaySE(string se_name)
    {
        string path = "Audio/SE/" + se_name;
        AudioClip clip = (AudioClip)Resources.Load(path);

        if (clip != null)
        {
            _seAudio.PlayOneShot(clip);
        }
        else
        {
            Debug.Log(se_name + "は見つかりません");
        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="bgm_name">BGMの名前</param>
    public void PlayBGM(string bgm_name)
    {
        string path = "Audio/BGM/" + bgm_name;
        AudioClip clip = (AudioClip)Resources.Load(path);

        if (clip != null)
        {
            _bgmAudio.clip = clip;
            _bgmAudio.Play();
        }
        else
        {
            Debug.Log(bgm_name + "は見つかりません");
        }
    }
}
