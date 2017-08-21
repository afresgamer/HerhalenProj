using System.Collections;
using UnityEngine;

public class LightEvent : MonoBehaviour {

    Light[] lightS;
    [SerializeField,Tooltip("ライトの再点灯の待機時間"),Header("ライトの再点灯の待機時間")]
    private float Wait_time = 3;
    [SerializeField, Tooltip("ライトのメッシュ情報"), Header("ライトのメッシュ情報")]
    private GameObject lightObj;
    private MeshRenderer[] meshS;
    //[SerializeField]
    //private float Light_Speed = 1;

    void Start()
    {
        lightS = GetComponentsInChildren<Light>();
        meshS = lightObj.GetComponentsInChildren<MeshRenderer>();
    }
	
	void Update ()
    {
        //Debug.Log(meshS.Length);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("hit");
            //Debug.Log("Shine Light");
            foreach (Light l in lightS)
            {
                l.intensity = 0;
                //Debug.Log(l.range);
            }
            foreach(MeshRenderer mesh in meshS)
            {
                mesh.material.DisableKeyword("_EMISSION");
                Debug.Log("light is vanish");
            }
            StartCoroutine("Test_Light_Eventer");
            
        }
    }

    public IEnumerator Test_Light_Eventer()
    {
        yield return new WaitForSeconds(Wait_time);
        foreach (Light l in lightS)
        {
            l.intensity = 3;
            //Debug.Log(l.range);
        }
        foreach(MeshRenderer mesh in meshS)
        {
            mesh.material.EnableKeyword("_EMISSION");
            Debug.Log("light is event");
        }
    }
}
