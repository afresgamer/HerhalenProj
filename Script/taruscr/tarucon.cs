using System.Collections;
using UnityEngine;

public class tarucon : MonoBehaviour
{
    public float sp = 5;
    Rigidbody gh;
    [SerializeField]
    private float wait = 1.5f;
    public static bool gg_Death;

    void Start()
    {
        gg_Death = false;
        gh = GetComponent<Rigidbody>();
        gh.AddForce(-transform.forward * sp);
    }

   void Update()
    {
        if (gg_Death) { Destroy(gameObject); }
        StartCoroutine(Tarudelete(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Tarudelete(GameObject g)
    {
        yield return new WaitForSeconds(wait);
        gg_Death = true;
    }

}
