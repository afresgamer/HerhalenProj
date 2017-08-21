using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrab : MonoBehaviour {

    public static bool isGrab = false;
    [SerializeField, Header("タッチでドアノブ判定")]
    private bool IsDoorGrab = false;
    public static bool isDoorTouchGrab = false;

    void Start()
    {
        isDoorTouchGrab = IsDoorGrab;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDoorTouchGrab && other.gameObject.tag == "Player_Hand")
        {
            isGrab = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDoorTouchGrab && other.gameObject.tag == "Player_Hand")
        {
            isGrab = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDoorTouchGrab && other.gameObject.tag == "Player_Hand")
        {
            isGrab = false;
        }
    }

}
