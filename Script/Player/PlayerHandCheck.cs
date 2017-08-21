using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandCheck : MonoBehaviour {

    //両手の判定格納変数
    private bool Girl_LeftHand;
    private bool Girl_RightHand;
    //両手フラグ
    public static bool Both_Hand = false;
    //左手フラグ
    public static bool LeftHand = false;
    //右手フラグ
    public static bool RightHand = false;

    void Start()
    {
        //フラグ管理でここに条件を拡張する形
        Girl_RightHand = Girl_Hand.seize_flag;
        Girl_LeftHand = Door.isButtonDown;
    }

    void Update ()
    {
        //左手
        if (Girl_LeftHand) { LeftHand = true; }
        //右手
        else if (Girl_RightHand) { RightHand = true; }
        //両手
        else if(Girl_LeftHand && Girl_RightHand) { Both_Hand = true; }

        else
        {
            Both_Hand = false;
            LeftHand = false;
            RightHand = false;
        }
	}
}
