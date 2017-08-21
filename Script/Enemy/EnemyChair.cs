using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChair : MonoBehaviour
{
    //化け物
    public GameObject Bakemono;
    //タイマー値保存変数
    public float timer = 0;
    //出すか出さないか
    bool isAppear = false;

    void Update()
    {
        if (timer > 10) return;

        if (isAppear == true)
        {
            //タイマー開始
            timer += Time.deltaTime;
            //10秒以上カウントしたか
            if (timer > 10)
            {
                //bakemono出す
                Bakemono.SetActive(true);
               
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isAppear = true;
            Debug.Log("kita");
        }

    }
}