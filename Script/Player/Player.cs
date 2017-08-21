using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Blur Blur;

    [SerializeField, Tooltip("キーボード入力オンオフ"), Header("キーボード入力オンオフ")]
    private bool Keyboard_MoveOn = false;

    Vector2 X_oculus;

    //コントローラー振動関係(テスト)
    [SerializeField, Header("振動用サウンド")]
    private AudioClip audioClip;
    OVRHapticsClip ovrHapticsClip;

    //コントローラーインデックス
    public static int Left_controller = 0;
    public static int Right_controller = 1;

    void Start()
    {
        Blur.enabled = false;//ブラー初期化
        //ovrHapticsClip = new OVRHapticsClip(audioClip);
        OVRManager.display.RecenterPose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        { OVRManager.display.RecenterPose(); }
        //コントローラー振動テスト用(終了取りあえずは振動させられた音は必要だけど)
        //if (audioClip != null && OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        //{ OVRHaptics.RightChannel.Mix(ovrHapticsClip); OVRHaptics.LeftChannel.Mix(ovrHapticsClip); }
        //コントローラー振動テストその２(自分で作った振動でコントローラーを振動させる長さと振動の強さも作れるのでけっこういいかも)
        //if(OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        //{ OVRHaptics.Channels[Right_controller].Mix(Vibrate_Create(128, 32 ,false)); OVRHaptics.Channels[Left_controller].Mix(Vibrate_Create(128, 32 ,false)); Debug.Log("振動中"); }
    }

    void FixedUpdate()
    {
        Default_Move();
        Oculus_Move(gameObject.transform);
        Oculus_Rot(gameObject.transform);
    }

    public void Default_Move()
    {
        //移動(Keyboard)
        if (Keyboard_MoveOn)
        {
            float v = Input.GetAxis("Vertical");
            if (v >= 0.5f) { transform.position += transform.forward * v * Time.deltaTime; }
            else if (v <= -0.5f) { transform.position += transform.forward * v * Time.deltaTime; }
        }
    }

    public void Oculus_Move(Transform obj_Move)
    {
        //移動(Oculus Touch)
        if (!Keyboard_MoveOn)
        {
            X_oculus = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            if (X_oculus.y >= 0.5f)
            {
                //Debug.Log("左上");
                obj_Move.position += obj_Move.transform.forward * X_oculus.y * Time.deltaTime;
            }
            if (X_oculus.y <= -0.5f)
            {
                //Debug.Log("左下");
                obj_Move.position += obj_Move.transform.forward * X_oculus.y * Time.deltaTime;
            }
        }
    }

    public void Oculus_Rot(Transform obj)
    {
        //方向転換
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || Input.GetKey(KeyCode.A))
        {
            Blur.enabled = true;
            //Debug.Log("左左");
            obj.Rotate(new Vector3(0.0f, Mathf.DeltaAngle(obj.rotation.y, obj.rotation.y - 45) * Time.deltaTime, 0.0f));
            //obj.rotation = Quaternion.Euler(0, transform.rotation.y + speed * Time.deltaTime, 0);
        }
        else if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight) || Input.GetKey(KeyCode.D))
        {
            Blur.enabled = true;
            //Debug.Log("左右");
            obj.Rotate(new Vector3(0.0f, Mathf.DeltaAngle(obj.rotation.y, obj.rotation.y + 45) * Time.deltaTime, 0.0f));
        }
        else { Blur.enabled = false; }
    }

    //振動用関数
    public void Vibrate_cotroller(OVRHapticsClip Hapticsclip, int cotroller)
    {
        OVRHaptics.Channels[cotroller].Mix(Hapticsclip);
    }

    //振動作成用関数
    public OVRHapticsClip Vibrate_Create(byte vibrating_count, int vibrating_second, bool IsRandom)
    {
        byte[] vibration = new byte[vibrating_second];//振動の長さ
        for (int i = 0; i < vibration.Length; i++)
        {
            if (IsRandom)//ランダム生成の振動数
            {
                vibration[i] = (byte)Random.Range(0, vibrating_count);//振動の強さ
            }
            else//指定方式の振動数
            {
                vibration[i] = vibrating_count;//振動の強さ
            }
        }

        return new OVRHapticsClip(vibration, vibration.Length);
    }

}
