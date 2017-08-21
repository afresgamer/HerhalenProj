using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField, Tooltip("周るスピード"), Header("周るスピード")]
    private float Speed = 1;
    [SerializeField, Tooltip("回転スピード"), Header("回転スピード")]
    private float Rot_Speed = 1;
    public static bool Goal = false;
    public float timer;
    private float movetimer;
    //public static bool ReStart = false;
    [SerializeField]
    private EnemyManager enemyManager;
    

    private void Update()
    {
        //タイムカウント
        timer += Time.deltaTime;
        //もしも2秒以上だったら
        if (timer > 2)
        //移動する
        {
            movetimer += Time.deltaTime;
            EnemyMove(enemyManager.EnemyTargetPosS[EnemyManager.count]);
            Destroy(this.gameObject, 15f);
        }
    }

    public void EnemyMove(Transform tra)
    {
        transform.position = Vector3.MoveTowards(transform.position, tra.position, Time.deltaTime * Speed);
        Quaternion targetRotation = Quaternion.LookRotation(tra.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * Rot_Speed);
    }

    internal void EnemyMove(object v, Transform target)
    {
        throw new NotImplementedException();
    }

    internal static void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}
