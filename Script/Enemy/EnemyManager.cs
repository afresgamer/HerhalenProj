using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
        
    [Tooltip("回る順番"), Header("回る順番")]
    public Transform[] EnemyTargetPosS;
    public static int length = 0;
    public static Transform target;
    public static int count = 0;

    
    //[SerializeField]
    //private Enemy enemy;

    //public static int Start_count;

    [SerializeField, Tooltip("到着してからの待機時間"), Header("到着してからの待機時間")]
    private float waittime = 3;

    void Start () {
        target = EnemyTargetPosS[0];
        length = EnemyTargetPosS.Length - 1;
    }
	
	void Update () {
        //位置の決定
        //Debug.Log(count);
        if (count < length) target = EnemyTargetPosS[count];
        //enemy.EnemyMove(!Enemy.Goal, target);
    }

    public IEnumerator EnemyChangeMove()
    {
        yield return new WaitForSeconds(waittime);
    }

}
