using UnityEngine;

public class TwoDoor : MonoBehaviour {

    Animator[] animS;
    bool near;
    public static bool isButton;
	
	void Start () {
        animS = GetComponentsInChildren<Animator>();
        isButton = false;
        //Debug.Log("anim init");
	}
	
	void Update () {
        if(DoorGrab.isDoorTouchGrab && isButton)
        {
            if (DoorGrab.isGrab)
            {
                foreach (Animator anim in animS)
                {
                    anim.SetBool("D_Open", near);
                    //Debug.Log("TwoDoor");
                }
            }
        }

        if (!DoorGrab.isDoorTouchGrab && isButton)
        {
            foreach (Animator anim in animS)
            {
                anim.SetBool("D_Open", near);
                //Debug.Log("TwoDoor");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            near = true;
            //Debug.Log ("hit");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = true;
            if(OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                isButton = true;
            }
            //Debug.Log("stay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = false;
            isButton = false;
            foreach(Animator anim in animS)
            {
                anim.SetBool("D_Open", isButton);
            }
            //Debug.Log("out");
        }
    }
}
