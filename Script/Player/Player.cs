using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {

    [SerializeField]
    private Blur Blur;

    Rigidbody m_Rigidbody;

    public static Vector2 X_oculus;

    //public static Vector3 prevPos;
    //public static Vector3 nowPos;
    //[HideInInspector]
    //public Vector3 pos;
    //[HideInInspector]
    //public float prevPosDistance;

    void Start () {
        
        Blur.enabled = false;//ブラー初期化
        
        m_Rigidbody = GetComponent<Rigidbody>();

        OVRManager.display.RecenterPose();
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F5))
        {
            OVRManager.display.RecenterPose();
        }
    }

    private void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        Oculus_Move(gameObject.transform,m_Rigidbody);
        Oculus_Rot(gameObject.transform, m_Rigidbody);
    }

    public void Oculus_Move(Transform obj_Move,Rigidbody _rb)
    {
        //移動(Keyboard)
        float v = Input.GetAxis("Vertical");
        if(v >= 0.5f) { transform.position += transform.forward * v * Time.deltaTime; }
        else if(v <= -0.5f) { transform.position += transform.forward * v * Time.deltaTime; }
        //移動(Oculus Touch)
        X_oculus = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

        //float x = 0.0f;
        //float z = 0.0f;

        if (X_oculus.y >= 0.5f)
        {
            //Debug.Log("左上");
            obj_Move.position += obj_Move.transform.forward * X_oculus.y  * Time.deltaTime;
        }
        if (X_oculus.y <= -0.5f)
        {
            //Debug.Log("左下");
            obj_Move.position += obj_Move.transform.forward * X_oculus.y  * Time.deltaTime;
        }
        
    }

    public void Oculus_Rot(Transform obj,Rigidbody _rb)
    {
        float x = 0.0f;
        float z = 0.0f;
        //方向転換
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || Input.GetKey(KeyCode.A))
        {
            Blur.enabled = true;
            //Debug.Log("左左");
            obj.Rotate(new Vector3(0.0f, Mathf.DeltaAngle(obj.rotation.y, obj.rotation.y - 45) * Time.deltaTime * 2, 0.0f));
        }
        else if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight) || Input.GetKey(KeyCode.D))
        {
            Blur.enabled = true;
            //Debug.Log("左右");
            obj.Rotate(new Vector3(0.0f, Mathf.DeltaAngle(obj.rotation.y, obj.rotation.y + 45) * Time.deltaTime * 2, 0.0f));
        }
        else { Blur.enabled = false; }

        _rb.velocity = z * obj.forward + x * obj.right;//方向軸
    }
}
