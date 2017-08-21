using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clip;
    private static AudioManager instanse = null;
    public static AudioManager Instanse
    {
        get
        {
            if (instanse == null)
            {
                instanse = new AudioManager();
            }
            return instanse;
        }
    }

    void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}

    public AudioClip GetAudioClip(string _name)
    {
        AudioClip tmp = null;

        for(int i = 0; i < clip.Length; i++)
        {
            // もし名前とデータ名があってたら
            　　// tmpにデータをいれて返してあげる
        }

        return tmp;
    }
}
