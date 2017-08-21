using UnityEngine;

public class Door : MonoBehaviour {

    Animator anim;
    bool near;
    public static bool isSisterMove;
    public static bool isButtonDown;

	void Start () {
        isButtonDown = false;
        isSisterMove = false;
        anim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        if (DoorGrab.isDoorTouchGrab && isButtonDown)
        {
            if (DoorGrab.isGrab)
            {
                anim.SetBool("D_Open", near);
                Debug.Log("anim start");
            }
        }
        if (!DoorGrab.isDoorTouchGrab && isButtonDown) { anim.SetBool("D_Open", near); }

        //Debug.Log("door");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = true;
            //Debug.Log ("hit");
        }
        if(other.gameObject.tag == "Sister")
        {
            isSisterMove = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = true;
            if ( OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) || Input.GetKeyDown(KeyCode.LeftControl))
            { isButtonDown = true; }
            //Debug.Log ("hit");
        }
        if (other.gameObject.tag == "Sister")
        {
            isSisterMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = false;
            isButtonDown = false;
            anim.SetBool("D_Open", isButtonDown);
            //Debug.Log("out");
        }
        if (other.gameObject.tag == "Sister")
        {
            isSisterMove = false;
        }
    }
}
