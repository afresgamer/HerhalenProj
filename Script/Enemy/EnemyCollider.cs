using UnityEngine;

public class EnemyCollider : MonoBehaviour {

    public static bool isBakemono_change = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player_Hand")
        {
            isBakemono_change = true;
            Debug.Log(isBakemono_change);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player_Hand")
        {
            isBakemono_change = false;
            Debug.Log(isBakemono_change);
        }
    }
}
