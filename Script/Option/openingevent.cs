using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openingevent: MonoBehaviour {

    Animator getani;
    public bool stop;
    public bool hit;
    [SerializeField]Image imagebox;
    [SerializeField]float speed = 0.3f;
    [SerializeField] GGFade1 ggfade;

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
        if (stop == true) { colorout(); }
        if (ggfade.alfa < 0) { stop = false; }

        if (Input.GetKeyDown("f") || OVRInput.Get(OVRInput.RawButton.B))
        {
            hit = true;
        }
        if (hit == true) { colorin(); }
        if (ggfade.alfa > 1) { hit = false; }


    }

    public void colorout()//徐々に明るくなっていく
    {
            ggfade.alfa -= speed * Time.deltaTime;
            imagebox.GetComponent<Image>().color = new Color(ggfade.red, ggfade.green, ggfade.blue, ggfade.alfa);
    }

    public void colorin()//徐々に暗くなっていく
    {
        ggfade.alfa += speed * Time.deltaTime;
        imagebox.GetComponent<Image>().color = new Color(ggfade.red, ggfade.green, ggfade.blue, ggfade.alfa);
    }

}
