using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField]
        private GameObject frontPivot = null;

        private Enemy enemy;

        private void Start(){
            enemy = transform.parent.GetComponent<Enemy>();
        }

        private void Update()
        {
            transform.forward = -Camera.main.transform.forward;

            float maxHp = enemy.GetMaxHp();
            float hp = enemy.GetHp();

            frontPivot.transform.localScale = new Vector3(hp / maxHp, 1f, 1f);
        }
    }
}
