using UnityEngine;
using RootMotion.FinalIK;

public class TargetAttacher : MonoBehaviour {

    [Tooltip("finalIK手の情報"), Header("finalIK手の情報")]
    private FABRIK fabRik;

    [SerializeField, Tooltip("肩の可動範囲"), Header("肩の可動範囲")]
    private RotationLimitAngle UpperLimitAngle;
    [SerializeField,Tooltip("肩のオンオフ"),Header("肩のオンオフ")]
    private bool UpperCheck;

    [SerializeField, Tooltip("肘の可動範囲"), Header("肘の可動範囲")]
    private RotationLimitHinge ArmLimitAngle;
    [SerializeField,Tooltip("肘のオンオフ"),Header("肘のオンオフ")]
    private bool ArmCheck;

    [SerializeField, Tooltip("手の可動範囲"), Header("手の可動範囲")]
    private RotationLimitAngle HandLimitAngle;
    [SerializeField, Tooltip("手のオンオフ"), Header("手のオンオフ")]
    private bool HandCheck;

    void Start()
    {
        fabRik = GetComponent<FABRIK>();
    }

    private void Update()
    {
        if (Girl_Hand.seize_flag)
        {
            //Debug.Log("hand move with girl hand");
            fabRik.enabled = true;
            UpperLimitAngle.enabled = UpperCheck;
            ArmLimitAngle.enabled = ArmCheck;
            HandLimitAngle.enabled = HandCheck;
        }
        else if (!Girl_Hand.seize_flag)
        {
            //Debug.Log("hand is alone");
            fabRik.enabled = false;
            UpperLimitAngle.enabled = UpperCheck;
            ArmLimitAngle.enabled = ArmCheck;
            HandLimitAngle.enabled = HandCheck;
        }
    }
}
