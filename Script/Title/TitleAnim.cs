using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleAnim : MonoBehaviour {
    //タイトルの処理
    Animator anim;
    bool titleStart;

    [Tooltip("女の子"), Header("女の子")]
    private Chase_Girl chase_Girl;
    [SerializeField, Tooltip("メイン初期位置"), Header("メイン初期位置")]
    private Transform MainStartPos;
    [SerializeField, Tooltip("フェード終了待機時間"), Header("フェード終了待機時間")]
    private float waitTime = 2;

    void Start () {
        anim = GetComponent<Animator>();
        titleStart = false;
        chase_Girl = FindObjectOfType<Chase_Girl>();
        chase_Girl.enabled = false;
	}

	void Update () {
        anim.SetBool("TitleStart", titleStart);
        //Debug.Log(OpeningEvent.Hit);
        //Debug.Log(Girl_Hand.title_flag);
        if (TitleButton.title_flag || OVRInput.GetDown(OVRInput.RawButton.Start) || Input.GetKeyDown(KeyCode.F11)){ OpeningEvent.Hit = true; StartCoroutine(StartMove()); }
    }
    
    public IEnumerator StartMove()
    {
        yield return new WaitForSeconds(waitTime);
        FindObjectOfType<Player>().transform.position = MainStartPos.position;
        yield return new WaitForSeconds(waitTime / 2);
        OpeningEvent.Stop = true;
        chase_Girl.enabled = true;
        //Debug.Log("MAINGAME START!!!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            titleStart = true;
            //Debug.Log("GAMESTART");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            titleStart = true;
            //Debug.Log("GAMESTART");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            titleStart = false;
            //Debug.Log("NOT GAMESTART");
        }
    }
}
