using UnityEngine;
using UnityEngine.VR;

public class Door : MonoBehaviour {

    Animator anim;
    bool near;

    //予備システム(入力判定めんどいのでVR動く動かないによって値を変えてますよ 今は使ってないけど)
    bool OnVRInput_Useing()
    {
        bool Isvr = VRDevice.isPresent ? OVRInput.GetDown(OVRInput.RawButton.A) : Input.GetKeyDown(KeyCode.Space);
        return Isvr;
    }

	void Start () {
        anim = GetComponent<Animator>();
	}

    //goto ドアは今後のことで一番処理が増えるので、ドアの角度を調整するようにしたい(将来的に)
    void Update()
    {
        if (near)
        {
            anim.SetBool("D_Open", true);
            //Debug.Log("Open");
        }
        /*else if(!near)
        {
            anim.SetBool("D_Open", false);
            //Debug.Log("Close");
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = true;
			//Debug.Log ("hit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            near = false;
            Debug.Log("out");
        }
    }
}
