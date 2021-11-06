using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    private EnemyAI _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.StopAttack();
        }
    }

}
