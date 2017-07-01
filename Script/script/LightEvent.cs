﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour {

    Light[] lightS;
    [SerializeField]
    private float Wait_time = 3;
    //[SerializeField]
    //private float Light_Speed = 1;
    
	void Start () {
        lightS = GetComponentsInChildren<Light>();
	}
	
	
	void Update ()
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("hit");
            //Debug.Log("Shine Light");
            foreach (Light l in lightS)
            {
                l.range = 0;
                //Debug.Log(l.range);
            }
            StartCoroutine("Test_Light_Eventer");
            
        }
    }

    public IEnumerator Test_Light_Eventer()
    {
        yield return new WaitForSeconds(Wait_time);
        foreach (Light l in lightS)
        {
            l.range = 6;
            //Debug.Log(l.range);
        }
    }
}