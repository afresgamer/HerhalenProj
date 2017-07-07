using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    
    //イベント関係呼び出し(将来的にはここら辺は一つにまとめる予定)
    [SerializeField, Header("ライトイベント")]
    private BoxCollider Light_box;
    [SerializeField,Tooltip("樽イベント"),Header("樽イベント")]
    private BoxCollider taru_box;

    private int Light_count = 1;//ライト用のインデックス
    private int Barrel_count = 2;//樽用のインデックス
    [SerializeField]
    private int End_count = 2;
    //現在のループ回数
    public static int count = 0;
    //エンドロール用(絶対に変えるな)
    private string scene = "GameClear";

    void Start ()
    {
        Light_box.enabled = false;
        taru_box.enabled = false;
    }

    void Update()
    {
        //2周目になったらライトイベント起動
        if(count == Light_count) {
            Light_box.enabled = true;
            Debug.Log("LIGHT ON");
        }
        else {
            Light_box.enabled = false;
        }
        
        //3周目になったら樽のイベント起動
        if(count == Barrel_count) {
            //Debug.Log("BARREL ON");
            taru_box.enabled = true;
        }
        else { taru_box.enabled = false; }

        //一定回数ループしたらエンドロールステージ
        if(count > End_count) { ChangeScene(scene); }
    }

    void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }

}
