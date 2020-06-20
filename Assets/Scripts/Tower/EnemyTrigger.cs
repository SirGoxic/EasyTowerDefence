using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Tower
{
    public class EnemyTrigger : MonoBehaviour
    {
        private void OnTriggerStay(Collider c){
            transform.parent.GetComponent<Tower>().EnemyInTrigger(c);
        }
    }
}
