using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Tower
{
    public class BaseTower : Tower
    {
        [SerializeField]
        private GameObject shootTrace = null;

        [SerializeField]
        private Transform shootHead = null;

        [SerializeField]
        private float traceLife = 0.1f;

        private void Update(){
            if(shooted){
                GameObject tempShootTrace = (GameObject)Instantiate(shootTrace);
                LineRenderer line = tempShootTrace.GetComponent<LineRenderer>();

                line.SetPosition(0, shootHead.position);
                line.SetPosition(1, lastHitedEnemyPos);

                Destroy(tempShootTrace, traceLife);
                shooted = false;
            }
        }
    }
}
