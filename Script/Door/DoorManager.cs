using UnityEngine;

public class DoorManager : MonoBehaviour {

    [SerializeField,Tooltip("ドアNumberの順番通りにアタッチして下さい")]
    private Door[] door;
    //ドアのオンオフの切り替え
    bool OnDoor = true;
    bool OffDoor = false;
    //ドアイベント用のインデックス
    [SerializeField] private int EventDoor = 3;

    void Start () {
        //ドアがアタッチされてなかった強制的にぶっこみ、ドアがなかったらエラー出す
        if(door == null)
        {
            door = FindObjectsOfType<Door>();
            if(door.Length == 0) {
                Debug.LogError("doorないから入れろ");
            }
        }
        //ドアの初期化(スタートのドア以外は動かない)
        for (int i = 1; i < door.Length; i++)
        {
            door[i].enabled = OffDoor;
        }
        door[0].enabled = OnDoor;
	}

    
	void Update () {
        //2周目以降はゲームスタート地点に行けないようにする
		if(GameController.count > 0)
        {
            door[0].enabled = OffDoor;
            door[EventDoor].enabled = OnDoor;//入口近くのドアだけ起動
        }
	}
}
