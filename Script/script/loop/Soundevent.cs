
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundevent : MonoBehaviour {

    public GameObject gg_soundbox01;//宣言
    public GameObject gg_soundbox02;//宣言
    AudioClip audioClip1;//宣言
    AudioSource audioSource01,audioSource02;//宣言
    Soundcontroller soucon;//宣言

    void Start ()
    {
        GameObject gg_box = GameObject.Find("soundcontroller");
        soucon = gg_box.GetComponent<Soundcontroller>();
        audioSource01 = gg_soundbox01.GetComponent<AudioSource>();
        audioSource02 = gg_soundbox02.GetComponent<AudioSource>();
    }

	void Update ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameController.count == 1)
            {
                audioClip1 = soucon.audioclip01[2];
                audioSource01.clip = audioClip1;
                audioSource01.Play();
            }
            else
            {
                audioSource01.loop = false;
            }

            if (GameController.count == 2)
            {
                audioClip1 = soucon.audioclip01[1];
                audioSource02.clip = audioClip1;
                audioSource02.Play();
                audioSource02.loop = true;
            }
            else
            {
                audioSource02.loop = false;
            }
        }
    }

}
