using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animescr : MonoBehaviour {
    Animator anime;

    public GameObject bakemno;

	void Start ()
    {
       anime = bakemno.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("l")) { anime.SetBool("idou", true); }//確認用
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anime.SetBool("idou",true);
        }
    }
}
