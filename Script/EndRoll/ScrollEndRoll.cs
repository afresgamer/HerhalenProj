using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollEndRoll : MonoBehaviour {

    RectTransform rt;
    public float BG_Speed = 1;
    bool Flag = false;

	void Start () {
        rt = GetComponent<RectTransform>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q) || OVRInput.Get(OVRInput.RawButton.A)) {
            Flag = true;
            Debug.Log("TRUE");
        }

        if (Flag)
        {
            Debug.Log("Moveing");
            rt.anchoredPosition += new Vector2(0, BG_Speed);
            if (rt.anchoredPosition.y >= 700) { rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 700); }
        }
	}
    
}
