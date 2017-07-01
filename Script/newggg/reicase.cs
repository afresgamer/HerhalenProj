using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class reicase : MonoBehaviour
{

    Vector3 oclpos;
    public LayerMask mask;
    //Ray ray;
    //public GameObject camera;

    void Start ()
    {
        
    }
	

	void Update ()
    {
        rayCastText();
	}

    public void rayCastText()
    {
        oclpos = InputTracking.GetLocalPosition(VRNode.CenterEye);
        Ray ray = new Ray(oclpos,transform.forward);

        RaycastHit getObj;
        
        if (Physics.Raycast(ray, out getObj,100.0f,mask))
        {
            Debug.Log(getObj.collider.name);

        }
        
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(oclpos, camera.transform.forward * 100);
    }*/
}
