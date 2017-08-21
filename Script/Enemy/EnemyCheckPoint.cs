using UnityEngine;

public class EnemyCheckPoint : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(!Enemy.Goal)
            {
                EnemyManager.count++;
            }
            //else if (EnemyManager.count >= EnemyManager.length + 1)
            //{
            //    EnemyManager.count = 0;
            //    Enemy.Goal = false;
            //}
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Enemy.Goal) { /*Enemy.Goal = false;*/  }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Enemy.Goal) { /*Enemy.Goal = false; EnemyManager.count = 0;*/ }
        }
    }
}
