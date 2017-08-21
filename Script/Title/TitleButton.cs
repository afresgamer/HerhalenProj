using UnityEngine;

public class TitleButton : MonoBehaviour {

    public static bool title_flag;

    bool button_Flag;

    void Start () {
        button_Flag = false;
        title_flag = false;
	}
	
	void Update () {
        if (button_Flag && (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)))
        {
            //Debug.Log("Have player hand");
            title_flag = true;
        }
        else { title_flag = false; }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Enter player hand");
            button_Flag = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Stay player hand");
            button_Flag = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Exit player hand");
            button_Flag = false;
        }
    }
}
