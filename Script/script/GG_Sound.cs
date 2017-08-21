using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_Sound : MonoBehaviour {

    AudioSource bgm;
    [SerializeField]
    private AudioSource[] se;
    [SerializeField]
    private bool audio_Loop = false;
    [SerializeField]
    private float _volume = 1;

    void Start () {
        bgm = GetComponent<AudioSource>();
        bgm.loop = audio_Loop;
        //bgm.pitch = 1;
        bgm.volume = _volume;
    }
	
    public void CreateSE(AudioSource source, bool flag, float volume)
    {
        source.loop = flag;
        source.volume = volume;
        if (flag)
        {
            source.Play();
        }
        else
        {
            source.PlayOneShot(source.clip);
        }
    }

	void Update () {
        CreateSE(se[0], audio_Loop, _volume);
	}
}
