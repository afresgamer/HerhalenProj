using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fadebutton : MonoBehaviour
{
    float alfa;
    public float speeds = 0.01f;
    float red, green, blue;
    bool kirikae01;
    bool kirikae02;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        kirikae01 = false;
        kirikae02 = false;
    }

    void Update()
    {
        //if (Input.GetKeyDown("w") ) { kirikae01 = true; Debug.Log("get"); }
        //if (Input.GetKeyDown("s") ) { kirikae02 = true; Debug.Log("set"); }
        //if (Input.GetKey(KeyCode.Joystick1Button1)) {/* kirikae01 = true;*/ Debug.Log("Button Push A"); StartCoroutine("Fade"); }
        //if (Input.GetKeyUp(KeyCode.Joystick1Button1)) { kirikae02 = true; Debug.Log("Button Push B"); }
        //if (Input.GetKey(KeyCode.Joystick1Button2)) { Debug.Log("Button Push X"); }
        //if (Input.GetKey(KeyCode.Joystick1Button3)) { Debug.Log("Button Push Y"); }

        //if (kirikae01 == true) { fadein(); }
        //if (kirikae02 == true) { fadeout(); }



    }
    /*改変時間9時間*/
    public IEnumerator Fadein()
    {
        alfa = 1;
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        yield return new WaitForSeconds(2);
        StartCoroutine("Fadeout");
    }

    public IEnumerator Fadeout()
    {
        alfa = 0;
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        yield return null;

    }

}