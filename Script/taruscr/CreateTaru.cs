using UnityEngine;

public class CreateTaru : MonoBehaviour {

    [SerializeField] private Transform AttachPoint;
    [SerializeField] private GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(prefab, AttachPoint.position, Quaternion.identity);
        }
    }
}
