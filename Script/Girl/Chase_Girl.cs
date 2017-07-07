using System.Collections.Generic;
using UnityEngine;
using RootMotion;
using RootMotion.FinalIK;

public class Chase_Girl : MonoBehaviour {

    public static Chase_Girl instance;
    //女の子の状態
    public enum GirlState { Default,Player_with }
    public static GirlState girlState;
    //手がプレイヤーの近くかどうか
    bool _near_flag;
    [SerializeField,Tooltip("現在のプレイヤー足元の位置"),Header("現在のプレイヤー足元の位置")]//現在のプレイヤー足元の位置
    private Transform player;
    [SerializeField, Tooltip("現在の左手の位置"), Header("現在の左手の位置")]//現在のplayer左手の位置
    private Transform p_Righthand;

    [SerializeField,Tooltip("Girlのスピード"),Header("Girlのスピード")]//Girlのスピード
    private float Smoothing = 1;

    Animator anim;
    Rigidbody rb;

    public enum AnimState//女の子状態
    {
        Walk,
        HandUp,
        StandNormal
    }

    [SerializeField,Tooltip("デフォルトのplayerとgirlの距離"),Header("デフォルトのplayerとgirlの距離")]
    private float startMoveRange = 1.0f;

    [SerializeField,Tooltip("軸回転速度"),Header("軸回転速度")]
    private float rotateSpeed = 3.0f;

    [SerializeField, Tooltip("playerと回転する速度"), Header("playerと回転する速度")]
    private float player_rotateSpeed = 1.0f;

    //動くかどうか
    bool isMove = false;
    //近いときに動くかどうか
    bool near_isMove = false;

    static Dictionary<AnimState, int> animParamHashDict = new Dictionary<AnimState, int>()
    {
        {AnimState.Walk, Animator.StringToHash("_Walk") },
        {AnimState.HandUp, Animator.StringToHash("Hand_Up") },
        {AnimState.StandNormal,Animator.StringToHash("Default") }
    };

    [SerializeField, Tooltip("女の子の初期位置"), Header("女の子の初期位置")]
    private Transform girl_InitPos;

    [Tooltip("全身IK情報"), Header("全身IK情報")]
    private FullBodyBipedIK fullBodyBipedIK;

    [SerializeField,Tooltip("肩の可動範囲"),Header("肩の可動範囲")]
    private RotationLimitAngle UpperLimitAngle;

    [SerializeField, Tooltip("肘の可動範囲"), Header("肘の可動範囲")]
    private RotationLimitHinge ArmLimitAngle;

    [SerializeField, Tooltip("手の可動範囲"), Header("手の可動範囲")]
    private RotationLimitAngle HandLimitAngle;

    //[SerializeField, Tooltip("手の握り"), Header("手の握り")]
    //private HandPoser handPoser;

    void Awake()
    {
        fullBodyBipedIK = GetComponent<FullBodyBipedIK>();
    }

    void Start () {
        girlState = GirlState.Default;
        _near_flag = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        UpperLimitAngle.enabled = false;
        ArmLimitAngle.enabled = false;
        HandLimitAngle.enabled = false;
        fullBodyBipedIK.enabled = false;
    }

    void FixedUpdate() {
        if (Girl_Hand.seize_flag) { girlState = GirlState.Player_with; }
        else { girlState = GirlState.Default; }
        switch (girlState)
        {
            case GirlState.Default:
                DefaultMove();
                break;
            case GirlState.Player_with:
                Player_WithMove();
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

        anim.SetBool(animParamHashDict[AnimState.HandUp], _near_flag );
        anim.SetBool(animParamHashDict[AnimState.Walk], isMove );

        if (isMove) // 距離の比較
        {
            transform.position = transform.position + transform.forward * Smoothing * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
        //怯える挙動
        //if (!isMove) { anim.SetBool(animParamHashDict[AnimState.StandNormal], !isMove );}
    }
    
    void Player_WithMove()
    {
        float distance = Vector3.Distance(transform.position, girl_InitPos.position);
        isMove = distance > 0.3f;

        Debug.Log(distance);

        anim.SetBool(animParamHashDict[AnimState.HandUp], _near_flag);
        //隣に来る処理(目標地点より離れてたら近づく)
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, girl_InitPos.position, Smoothing * Time.deltaTime);
            Debug.Log(isMove);
        }
        else
        {
            //女の子とplayerの手の位置を一緒の位置にする
            fullBodyBipedIK.enabled = true;
            HandLimitAngle.enabled = true;
            UpperLimitAngle.enabled = true;

            fullBodyBipedIK.references.leftHand.position = p_Righthand.position;
            fullBodyBipedIK.references.leftHand.rotation = p_Righthand.rotation;

            anim.SetBool(animParamHashDict[AnimState.Walk], Girl_Hand.seize_flag);

            if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
            {
                transform.SetParent(player);
                Quaternion targetRotation = Quaternion.LookRotation(player.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * player_rotateSpeed);
                
            }
            else if (!OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || !OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
            {
                transform.SetParent(null);
                fullBodyBipedIK.enabled = false;
                //handPoser.enabled = false;
                HandLimitAngle.enabled = false;
                UpperLimitAngle.enabled = false;
            }
            //移動
            transform.position = transform.position + transform.forward * Player.X_oculus.y * Time.deltaTime;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {  _near_flag = true; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") { _near_flag = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _near_flag = false;
            anim.SetBool(animParamHashDict[AnimState.Walk], false);
        }
    }
}
