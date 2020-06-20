using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Tower{
    public class Tower : MonoBehaviour
    {

        [SerializeField]
        protected float damage = 1f;
        private float startDamage = 1f;
        [SerializeField]
        protected float damageIncreaseFactor = 1.5f; 

        [SerializeField]
        protected float shootSpeed = 2f;
        private float startShootSpeed = 1f;
        [SerializeField]
        protected float shootSpeedIncreaseFactor = 1.2f; 

        [SerializeField]
        protected int levelUpPrice = 1;
        private int startLevelUpPrice = 1;
        [SerializeField]
        protected int levelUpPriceIncreaseFactor = 2;
        protected int level = 1;

        [SerializeField]
        protected GameObject selector = null;

        protected Vector3 lastHitedEnemyPos = Vector3.zero;

        protected bool timeToShoot = true;

        protected bool shooted = false;

        private void Start(){
            startDamage = damage;
            startShootSpeed = shootSpeed;
            startLevelUpPrice = levelUpPrice;
        }

        public bool LevelUp(){
            if(Player.Player.GetGold() >= levelUpPrice){
                Player.Player.RemoveGold(levelUpPrice);

                level++;
                ChangeLevel();

                return true;
            }
            return false;
        }

        public void RestartTower(){
            damage = startDamage;
            shootSpeed = startShootSpeed;
            levelUpPrice = startLevelUpPrice;

            level = 1;
        }

        protected void ChangeLevel(){
            damage += damageIncreaseFactor * (1f + level * 0.1f);
            shootSpeed += shootSpeedIncreaseFactor * (1f + level * 0.1f);
            levelUpPrice += (int)(levelUpPriceIncreaseFactor * (1f + level * 0.1f));
        }

        public int GetLevel(){
            return level;
        }

        public int GetLevelPrice(){
            return levelUpPrice;
        }

        public float GetDamage(){
            return damage;
        }

        public float GetShootSpeed(){
            return shootSpeed;
        }

        public void SelectorActive(bool active){
            selector.SetActive(active);
        }

        public void EnemyInTrigger(Collider c){
            Enemy.Enemy tempEnemy;
            if(c.TryGetComponent<Enemy.Enemy>(out tempEnemy)){
                if(timeToShoot){
                    tempEnemy.RemoveHp(damage);
                    
                    timeToShoot = false;

                    lastHitedEnemyPos = tempEnemy.transform.position;
                    shooted = true;

                    StartCoroutine(Shoot());
                }
            }
        }

        protected IEnumerator Shoot(){
            yield return new WaitForSeconds(1f / shootSpeed);
            timeToShoot = true;
        }
    }
}
