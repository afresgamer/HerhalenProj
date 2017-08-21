using UnityEngine;

public class Girl_Hand : MonoBehaviour {

    public static bool near_flag;
    [Tooltip("手が近くにあるか")]
    public static bool seize_flag;

    void Start () {
        near_flag = false;
        seize_flag = false;
    }
	
	void Update () {
        
        if (near_flag && (Input.GetKey(KeyCode.Space) || OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)))
        {
            //Debug.Log("Have player hand");
            seize_flag = true;
        }
        else { seize_flag = false; }

        //Debug.Log(seize_flag);
        //Debug.Log("Button : " + OVRInput.GetDown(OVRInput.Button.One));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Enter player hand");
            near_flag = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Stay player hand");
            near_flag = true;
         }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player_Hand")
        {
            //Debug.Log("Exit player hand");
            near_flag = false;
        }
    }
}
