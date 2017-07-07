using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : MonoBehaviour {

    Light[] lightS;
    [SerializeField]
    private float Wait_time = 3;
    [SerializeField, Tooltip("ライトのメッシュ情報"), Header("ライトのメッシュ情報")]
    private GameObject lightObj;
    private MeshRenderer[] meshS;
    //[SerializeField]
    //private float Light_Speed = 1;
    
	void Start () {
        lightS = GetComponentsInChildren<Light>();
        meshS = lightObj.GetComponentsInChildren<MeshRenderer>();
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
            foreach(MeshRenderer mesh in meshS)
            {
                mesh.material.EnableKeyword("_ENISTION");
                mesh.material.SetColor("_Emission_Color", new Color(0,0,0));
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
        foreach(MeshRenderer mesh in meshS)
        {
            mesh.material.EnableKeyword("_EMISSION");
            mesh.material.SetColor("_Emission_Color", new Color(1, 1, 1));
        }
    }
}
