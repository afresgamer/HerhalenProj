using UnityEngine;

public class LoopEvent : MonoBehaviour {

    GameObject startposition;
    [SerializeField, Tooltip("フェード処理"), Header("フェード処理")] private OpeningEvent fade;
    [SerializeField,Tooltip("女の子の位置"),Header("女の子の位置")] private Transform Girl;
    [SerializeField,Tooltip("スピード設定"),Header("スピード設定")] private GGFade1 ggfade;

	void Start ()
    {
        startposition = GameObject.FindGameObjectWithTag("PlayerStart");
	}

	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(fade.WaitFadeInOut(ggfade.Speed));
            GameController.count++;
            other.transform.position = startposition.transform.position;
            Girl.transform.position = startposition.transform.forward;
        }
    }

}
