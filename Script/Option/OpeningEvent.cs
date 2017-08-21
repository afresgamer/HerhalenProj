using UnityEngine;
using UnityEngine.UI;

public class OpeningEvent : MonoBehaviour {

    //public static OpeningEvent instance = null;

    Animator anim;
    public static bool init;
    public static bool Stop;
    public static bool Hit;
    Image image;
    [SerializeField] private GGFade ggfade;

    void Awake()
    {
        //フェード関係を管理
        /*if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        */
    }

    void Start()
    {
        init = false;
        Stop = false;
        Hit = false;
        image = FindObjectOfType<Image>();
        //Debug.Log(image.color.a);
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
    }

    void Update()
    {
        if (Input.GetKeyDown("p") || OVRInput.GetDown(OVRInput.RawButton.X)) { init = true; }
        //フェード初期化
        if (init) { Hit = false; Stop = false; image.color = new Color(image.color.r, image.color.g, image.color.b, 0); init = false; }
        //フェード基本処理
        if (Input.GetKeyDown("g") || OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Stop = true;
        }
        if (Stop == true) { Colorout(ggfade.Speed); }
        if (image.color.a < 0) { Stop = false; }

        if (Input.GetKeyDown("f") || OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Hit = true;
        }
        if (Hit == true) { Colorin(ggfade.Speed); }
        if (image.color.a > 1) { Hit = false; }
    }

    public void Colorout(float speed)//徐々に明るくなっていく
    {
        float a = image.color.a;
        a -= speed * Time.deltaTime;
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        //Debug.Log(a);
    }

    public void Colorin(float speed)//徐々に暗くなっていく
    {
        float a = image.color.a;
        a += speed * Time.deltaTime;
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        //Debug.Log(a);
    }

}
