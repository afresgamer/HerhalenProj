using System.Collections.Generic;
using UnityEngine;

public class Chase_Girl : MonoBehaviour
{

    //女の子の状態
    public enum GirlState { Default, Player_with, sister_bakemono_switch_Event }
    public static GirlState girlState;
    //手がプレイヤーの近くかどうか
    bool _near_flag;
    [SerializeField, Tooltip("現在のプレイヤー足元の位置"), Header("現在のプレイヤー足元の位置")]//現在のプレイヤー足元の位置
    private Transform player;

    [SerializeField, Tooltip("Girlのスピード"), Header("Girlのスピード")]//Girlのスピード
    private float Smoothing = 1;

    Animator anim;

    public enum AnimState//女の子状態
    {
        Walk,
        Hand_Raised,
        Hand_Grab,
        StandDefault,
        Sit_Down,
        Stand_Up
    }

    [SerializeField, Tooltip("デフォルトのplayerとgirlの距離"), Header("デフォルトのplayerとgirlの距離")]
    private float startMoveRange = 1.0f;

    [SerializeField, Tooltip("軸回転速度"), Header("軸回転速度")]
    private float rotateSpeed = 3.0f;

    [SerializeField, Tooltip("playerと回転する速度"), Header("playerと回転する速度")]
    private float playerWith_rotateSpeed = 1.0f;

    //動くかどうか
    bool isMove = false;

    static Dictionary<AnimState, int> animParamHashDict = new Dictionary<AnimState, int>()
    {
        {AnimState.Walk, Animator.StringToHash("_Walk") },
        {AnimState.Hand_Raised, Animator.StringToHash("Hand_Raised") },
        {AnimState.Hand_Grab, Animator.StringToHash("Hand_Grab") },
        {AnimState.StandDefault, Animator.StringToHash("Default") },
        {AnimState.Sit_Down, Animator.StringToHash("Sit_Down") },
        {AnimState.Stand_Up,Animator.StringToHash("Stand_Up") }
    };

    [SerializeField, Tooltip("妹の初期位置"), Header("妹の初期位置")]
    private Transform girlWalk_InitPos;

    [SerializeField, Tooltip("ドア前にいる時の妹の位置"), Header("ドア前にいる時の妹の位置")]
    private Transform girlDoor_frontPos;

    [SerializeField, Tooltip("妹と目標地点"), Header("妹と目標地点")]
    private float girl_ToInit_range = 0.3f;

    void Start()
    {
        girlState = GirlState.Default;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Debug.Log(girlState);
        if (_near_flag) { girlState = GirlState.Player_with; }
        else if (Sister_Event.isEnd) { girlState = GirlState.sister_bakemono_switch_Event; }
        else { girlState = GirlState.Default; }
        switch (girlState)
        {
            case GirlState.Default:
                DefaultMove();
                break;
            case GirlState.Player_with:
                Player_WithMove();
                break;
            case GirlState.sister_bakemono_switch_Event:
                Sister_Bakemono_Switch();
                break;
            default:
                break;
        }
    }

    void DefaultMove()
    {
        transform.SetParent(null);
        float sqrDistance = Vector3.SqrMagnitude(transform.position - player.position);
        isMove = sqrDistance > startMoveRange;

        //怯える挙動
        anim.SetBool(animParamHashDict[AnimState.StandDefault], !isMove);
        anim.SetBool(animParamHashDict[AnimState.Hand_Raised], _near_flag);
        anim.SetBool(animParamHashDict[AnimState.Walk], isMove);

        if (isMove) // 距離の比較
        {
            transform.position = transform.position + transform.forward * Smoothing * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    void Player_WithMove()
    {
        float distance = Vector3.Distance(transform.position, girlWalk_InitPos.position);
        isMove = distance > girl_ToInit_range;
        bool isInput = OVRInput.Get(OVRInput.RawButton.LThumbstickUp) || OVRInput.Get(OVRInput.RawButton.LThumbstickDown) 
            || OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || OVRInput.Get(OVRInput.RawButton.LThumbstickRight);

        //Debug.Log(distance);
        //アニメーション関係
        anim.SetBool(animParamHashDict[AnimState.Hand_Raised], _near_flag);
        anim.SetBool(animParamHashDict[AnimState.Hand_Grab], Girl_Hand.seize_flag);
        anim.SetBool(animParamHashDict[AnimState.StandDefault], false);
        anim.SetBool(animParamHashDict[AnimState.Walk], isInput);
        anim.SetBool(animParamHashDict[AnimState.Sit_Down], Sister_Event.isBegin);//転ぶ処理

        //手握る前に隣に来る処理(目標地点より離れてたら近づく)
        if (isMove　&& !Girl_Hand.seize_flag)
        {
            transform.position = Vector3.MoveTowards(transform.position, girlWalk_InitPos.position, Smoothing * Time.deltaTime);
            anim.SetBool(animParamHashDict[AnimState.Walk], true);
            //Debug.Log(isMove);
        }
        //手握ってるとき
        else if (Girl_Hand.seize_flag)
        {
            //移動
            transform.SetParent(player);
            //回転
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, playerWith_rotateSpeed * Time.deltaTime);
        }
        //ドアの前にいるとき
        else if (Door.isSisterMove)
        {
            //移動
            transform.position = Vector3.MoveTowards(transform.position, girlDoor_frontPos.position, Smoothing * Time.deltaTime);
            //回転
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, playerWith_rotateSpeed * Time.deltaTime);
        }
        //何もしてないとき
        else { transform.SetParent(null); }
    }

    void Sister_Bakemono_Switch()
    {
        //化け物と入れ替わりイベント用アニメーション
        anim.SetBool(animParamHashDict[AnimState.Sit_Down], Sister_Event.isBegin);

         //化け物と入れ替わりイベント用
        if (_near_flag)
        {
            if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                //現状は手で立ち上がるアニメーション
                Sister_Event.isEnd = false;
                isMove = true;
            }
        }
        if (isMove) { anim.SetBool(animParamHashDict[AnimState.Stand_Up], true); }
        else { anim.SetBool(animParamHashDict[AnimState.Stand_Up], false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _near_flag = true;
            //Debug.Log("Player is near"); 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _near_flag = true;
            //Debug.Log("Player is near"); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _near_flag = false;
            //Debug.Log("Player isn't near");
        }
    }
}
