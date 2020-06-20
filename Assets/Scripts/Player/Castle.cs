using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Player
{
    public class Castle : MonoBehaviour
    {
        [SerializeField]
        private GameObject deathPanel = null;

        [SerializeField]
        private Enemy.Spawner spawner = null;

        private void OnTriggerEnter(Collider c){
            Enemy.Enemy tempEnemy;
            if(c.gameObject.TryGetComponent<Enemy.Enemy>(out tempEnemy)){
                Player.RemoveHp(tempEnemy.GetDamage());
                spawner.DestroyEnemy(c.gameObject);
                if(Player.GetHp() <= 0){
                    deathPanel.SetActive(true);
                    spawner.StopGame();
                }
            }
        }
    }
}
