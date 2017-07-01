using System.Collections.Generic;
using UnityEngine;

public class Chase_Girl : MonoBehaviour {

    public static Chase_Girl instance;
    //女の子の状態
    public enum GirlState { Default,Player_with }
    public static GirlState girlState;
    //手がプレイヤーの近くかどうか
    bool _near_flag;
    [SerializeField,Tooltip("現在のプレイヤー足元の位置"),Header("現在のプレイヤー足元の位置")]//現在のプレイヤー足元の位置
    private Transform player;
    [SerializeField,Tooltip("Girlのスピード"),Header("Girlのスピード")]//Girlのスピード
    private float Smoothing = 1;

    Animator anim;
    Rigidbody rb;

    [SerializeField,Tooltip("手を繋いでるときの女の子とプレイヤーとの距離"), Header("手を繋いでるときの女の子とプレイヤーとの距離")]
    private float playerdistance = 10f;
    
    public enum AnimState//女の子状態
    {
        Walk,
        HandUp
    }

    [SerializeField,Tooltip("デフォルトのplayerとgirlの距離"),Header("デフォルトのplayerとgirlの距離")]
    private float startMoveRange = 1f;

    [SerializeField,Tooltip("軸回転速度"),Header("軸回転速度")]
    private float rotateSpeed = 3.0f;

    //動くかどうか
    bool isMove;

    static Dictionary<AnimState, int> animParamHashDict = new Dictionary<AnimState, int>()
    {
        {AnimState.Walk, Animator.StringToHash("_Walk") },
        {AnimState.HandUp, Animator.StringToHash("Hand_Up") }
    };

    //[Tooltip("全身IK情報"), Header("全身IK情報")]
    //private RootMotion.FinalIK.FullBodyBipedIK fullBodyBipedIK;

    //[SerializeField,Tooltip("肩の可動範囲"),Header("肩の可動範囲")]
    //private RootMotion.FinalIK.RotationLimitAngle rotationLimitAngle;

    //[SerializeField, Tooltip("手の握り"), Header("手の握り")]
    //private RootMotion.FinalIK.HandPoser handPoser;

    void Awake()
    {
        
    }

    void Start () {
        girlState = GirlState.Default;
        _near_flag = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //rotationLimitAngle.enabled = false;
        //handPoser.enabled = false;
        //fullBodyBipedIK = GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        //fullBodyBipedIK.enabled = false;
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

        anim.SetBool(animParamHashDict[AnimState.HandUp], _near_flag);
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
        float sqrDistance = Vector3.SqrMagnitude(transform.position - player.right);
        Vector3 targetdistance = player.right;
        isMove = sqrDistance > targetdistance.sqrMagnitude;

        anim.SetBool(animParamHashDict[AnimState.HandUp], _near_flag);
        //隣に来る処理
        if (isMove) {
            transform.position = Vector3.Slerp( transform.position , player.right / playerdistance, Smoothing * Time.deltaTime);
            Debug.Log(isMove);
        }

        //移動
        if (!isMove)
        {
            anim.SetBool(animParamHashDict[AnimState.Walk], Girl_Hand.seize_flag);
            Debug.Log(isMove);
            
            if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
            {
                transform.SetParent(player);
                //fullBodyBipedIK.enabled = true;
                //handPoser.enabled = true;
                //rotationLimitAngle.enabled = true;
            }
            else if(!OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || !OVRInput.Get(OVRInput.RawButton.LThumbstickRight))
            {
                transform.SetParent(null);
                //fullBodyBipedIK.enabled = false;
                //handPoser.enabled = false;
                //rotationLimitAngle.enabled = false;
            }
            Quaternion targetRotation = Quaternion.LookRotation(player.forward - player.forward / 4);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
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
            anim.SetBool("_Walk", false);
        }
    }

}
