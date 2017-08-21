using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHumanfigure : MonoBehaviour
{

    public GameObject Bakemono;
    public static bool isMove = false;
    

    void Start()
    {
        //anim = GetComponent<Animation>();
        //foreach (AnimationState state in anim)
        //{
        //    state.speed = 0.5F;
        //}
    }
    private void OnTriggerEnter(Collider col)
    {
        //Instantiate(Bakemono, Startpos.transform.position, Quaternion.identity);
        if (col.gameObject.tag == "Player")
        {
            Bakemono.SetActive(true);
            isMove = true;
            Debug.Log("吾輩はママであるっ!!!!!");
        }
    }

}

