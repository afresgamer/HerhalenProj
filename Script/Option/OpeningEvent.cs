using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningEvent : MonoBehaviour {

    Animator getani;
    public bool stop;
    public bool hit;
    [SerializeField] Image imagebox;
    [SerializeField] GGFade1 ggfade;
    [SerializeField] private float waitTime = 3;

    void Start()
    {
        ggfade.red = imagebox.GetComponent<Image>().color.r;
        ggfade.green = imagebox.GetComponent<Image>().color.g;
        ggfade.blue = imagebox.GetComponent<Image>().color.b;
        ggfade.alfa = 1;
        imagebox.GetComponent<Image>().color = new Color(ggfade.red, ggfade.green, ggfade.blue, ggfade.alfa);
    }

    void Update()
    {
        if (Input.GetKeyDown("g") || OVRInput.Get(OVRInput.RawButton.A))
        {
            stop = true;
        }
        if (stop == true) { Colorout(ggfade.Speed); }
        if (ggfade.alfa < 0) { stop = false; }

        if (Input.GetKeyDown("f") || OVRInput.Get(OVRInput.RawButton.B))
        {
            hit = true;
        }
        if (hit == true) { Colorin(ggfade.Speed); }
        if (ggfade.alfa > 1) { hit = false; }
    }

    public void Colorout(float speed)//徐々に明るくなっていく
    {
            ggfade.alfa -= ggfade.Speed * Time.deltaTime;
            imagebox.GetComponent<Image>().color = new Color(ggfade.red, ggfade.green, ggfade.blue, ggfade.alfa);
    }

    public void Colorin(float speed)//徐々に暗くなっていく
    {
        ggfade.alfa += ggfade.Speed * Time.deltaTime;
        imagebox.GetComponent<Image>().color = new Color(ggfade.red, ggfade.green, ggfade.blue, ggfade.alfa);
    }

    public IEnumerator WaitFadeInOut(float speed)//徐々に暗くなって明るくなる
    {
        if(ggfade.alfa == 1) { yield return new WaitForSeconds(waitTime); Colorout(speed); }
        else { Colorin(speed); }
        //yield break;
    }

}
