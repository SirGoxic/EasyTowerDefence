using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Player
{
    public class GameInic : MonoBehaviour
    {

        [SerializeField]
        private GameObject levelUpPanel = null;
        [SerializeField]
        private GameObject deathPanel = null;

        [SerializeField]
        private Enemy.Spawner spawner = null;

        [SerializeField]
        private List<Tower.Tower> towers = new List<Tower.Tower>();

        [SerializeField]
        private int hp = 1000;
        [SerializeField]
        private int gold = 0;

        private void Start(){
            Restart();
        }

        public void Restart(){

            levelUpPanel.SetActive(false);
            deathPanel.SetActive(false);

            Player.SetHp(hp);
            Player.SetGold(gold);
            Player.SetEnemyKilled(0);

            spawner.DestroyAllEnemys();

            spawner.StartGameCourutine();
            
            for(int i = 0; i < towers.Count; i++){
                towers[i].SetLevel(1);
            }
        }
    }
}
