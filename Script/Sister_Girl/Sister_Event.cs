using System.Collections;
using UnityEngine;

public class Sister_Event : MonoBehaviour {

    //SE情報

    //恐怖のBGM情報

    //女の子
    [SerializeField, Header("女の子")]
    private GameObject sister;
    //女の子のメッシュ情報
    [SerializeField, Header("女の子のメッシュ情報")]
    private GameObject sisterMesh;
    //化け物のメッシュ情報
    [SerializeField, Header("化け物のメッシュ情報")]
    private GameObject bakemono_mesh;
    //化け物のアニメーション
    Animator anim;
    //プレイヤー
    [SerializeField, Header("プレイヤー")]
    private GameObject player;
    //化け物に触れているかどうか
    [SerializeField, Header("化け物に触れているかどうか")]
    private EnemyCollider enemyCollider;
    //プレイヤーの視線
    Ray ray;
    //プレイヤーの視線衝突判定
    RaycastHit hit;
    //化け物のスピード
    [SerializeField, Header("化け物のスピード")]
    private float bakemono_speed = 0.1f;
    //視線判定距離
    [SerializeField, Header("視線判定距離")]
    private float sight_range = 5;
    [SerializeField, Header("妹に戻る離れる距離")]
    private float sister_Retrun_range = 3;
    [SerializeField, Header("フェード待機時間")]
    private float wait = 3;
    public static bool isBegin;
    public static bool isEnd;


	void Start () {
        isBegin = false;
        isEnd = false;
        bakemono_mesh.SetActive(false);
        enemyCollider.enabled = false;
        anim = bakemono_mesh.GetComponentInParent<Animator>();
        anim.enabled = false;
    }
	
	void Update () {
        //視線についてはカメラからとplayerの頭からの二つを想定中(カメラで処理してます)
        ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);

        //Debug.DrawRay(ray.origin, ray.direction * sight_range, new Color(1, 0, 1));
        
        if (isBegin)
        {
            Debug.Log("Event Start");
            //妹の動作を停止
            sister.transform.SetParent(null);
            sister.GetComponent<Chase_Girl>().enabled = false;
            //SEをならす
            StartCoroutine(FollowSound());
            //化け物出現と妹を動かなくする
            sisterMesh.SetActive(false);
            bakemono_mesh.SetActive(true);
            enemyCollider.enabled = true;
            //アニメーションの動作起動
            anim.enabled = true;
            anim.SetBool("Craw", true);
            //プレイヤーのほうに向ける
            Quaternion target = Quaternion.LookRotation(player.transform.position);
            bakemono_mesh.GetComponent<Transform>().rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
            if(Physics.Raycast(ray,out hit, sight_range))
            {
                //女の子を視線にとらえたらBGMがなる(一回視線を向けたら鳴る感じだと推測中)
                Debug.Log("Start BGM");
                //声は妹のまま再生
                Debug.Log("マッテヨォ…何でオイテクノ");
            }
            //プレイヤーと妹(化け物)の距離を求める
            float distance = Vector3.Distance(transform.position,player.transform.position);
            
            //距離比較と分岐
            if(distance > sister_Retrun_range)
            {
                Sister_Escape();
            }
            else if(EnemyCollider.isBakemono_change)
            {
                Bakemono_Hand_Grab();
            }
        }
	}

    public IEnumerator Wait_Sister_Escape()
    {
        yield return new WaitForSeconds(wait);
        Sister_Escape();
        yield return new WaitForSeconds(wait / 2);
        OpeningEvent.Stop = true;
    }

    void Sister_Escape()
    {
        //妹を出現させ、化け物を消す
        sisterMesh.SetActive(true);
        sister.GetComponent<Chase_Girl>().enabled = true;
        bakemono_mesh.SetActive(false);
        enemyCollider.enabled = false;
        //BGMを終了させて、デフォルトBGMに戻す
        Debug.Log("BGM Default");
        Debug.Log("何で置いてくの？(泣)");
        isBegin = false;
        isEnd = true;
    }

    void Bakemono_Hand_Grab()
    {
        //ここで化け物の手に近づいてるかどうかの判定によって処理する
        if (Input.GetKeyDown(KeyCode.X) || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            OpeningEvent.Hit = true;
            StartCoroutine(Wait_Sister_Escape());
        }
    }

    public IEnumerator FollowSound()
    {
        //声
        Debug.Log("キャッ!!!!");
        yield return new WaitForSeconds(1);
        //ドスンと転ぶ音
        Debug.Log("ドスン");
        yield return new WaitForSeconds(1);
        //妹の声
        Debug.Log("うぅ…痛いよぉ…");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isBegin = true;
            isEnd = false;
        }
    }
}
