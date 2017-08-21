
using UnityEngine;
using System.Collections;

public class LoopEvent : MonoBehaviour {

    GameObject startposition;
    GameObject girlposition;
    [Tooltip("フェード処理"), Header("フェード処理")]
    private OpeningEvent fade;
    [SerializeField,Tooltip("女の子の位置"),Header("女の子の位置")]
    private Transform Girl;
    [SerializeField, Tooltip("待機時間"), Header("待機時間")]
    private float waitTime = 3;
    GameObject housereference;
    GameObject getsoundcontroller;
    Soundcontroller soundcon;

    void Start ()
    {
        //Debug.Log(fade);
        startposition = GameObject.FindGameObjectWithTag("Respawn");
        girlposition = GameObject.FindGameObjectWithTag("GirlStart");

        housereference = GameObject.Find("GameContrller");//Hierarchy上のGameContrllerを所得
        startposition = GameObject.FindGameObjectWithTag("PlayerStart");
        girlposition = GameObject.FindGameObjectWithTag("GirlStart");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            GameController.count++;
          //  StartCoroutine(WaitMovePos());

            Houseswitch Hswitch = housereference.GetComponent<Houseswitch>();//Houseswitchの呼び出し
            Hswitch.housecheck(Hswitch.housebox[Hswitch.kaunto]);//Houseswitchのhousecheck起動Hswitch.kauntoの数値によって呼び出しが変動する
            Hswitch.kaunto++;//Houseswitchのkauntoをプラス1している
        }
    }
}
