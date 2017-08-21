using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houseswitch : MonoBehaviour {
    [SerializeField]
    public GameObject[] housebox;
    public int kaunto = 0;
    public int deskaunto = 0;
    void Start ()
    {
		
	}

	void Update ()
    {
        if (Input.GetKeyDown("j")) { housecheck(housebox[0]); }
	}

    public void housecheck(GameObject hoge)
    {
        if (kaunto == 0) { housebox[0].SetActive(true); }
        else if (kaunto == 1) { housebox[1].SetActive(true); }
        else if (kaunto == 2) { housebox[2].SetActive(true); }
        else if (kaunto == 3) { housebox[3].SetActive(true); }
        else if (kaunto == 4) { housebox[4].SetActive(true); }
       // else if (kaunto == 5) { housebox[5].SetActive(true); }
    }

    public void housedescheck(GameObject hoge)
    {
        if (deskaunto == 0) { housebox[0].SetActive(false); }
        else if (deskaunto == 1) { housebox[1].SetActive(false); }
        else if (deskaunto == 2) { housebox[2].SetActive(false); }
        else if (deskaunto == 3) { housebox[3].SetActive(false); }
        else if (deskaunto == 4) { housebox[4].SetActive(false); }
        else if (deskaunto == 5) { housebox[5].SetActive(false); }
    }

}
