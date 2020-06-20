using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField]
        protected float moveSpeed = 4f;
        [SerializeField]
        protected float moveSpeedIncreaseFactor = 1.002f; 
        [SerializeField]
        protected int damage = 1;
        [SerializeField]
        protected float damageIncreaseFactor = 1.02f;
        [SerializeField]
        protected float hp = 100f;
        protected float maxHp = 100f;

        [SerializeField]
        protected float hpIncreaseFactor = 1.02f;
        [SerializeField]
        protected int goldDrop = 2;
        [SerializeField]
        protected float goldDropIncreaseFactor = 1.02f;

        protected Path.Way way;

        protected bool wayGeted = false;

        protected int pointIndex = 0;

        protected Spawner spawner;

        public void SetWay(Path.Way way)
        {
            this.way = way;
            wayGeted = true;
        }

        public void SetWave(int waveNumber){
            moveSpeed += waveNumber * moveSpeedIncreaseFactor;

            damage += (int)(waveNumber * damageIncreaseFactor);

            hp += waveNumber * hpIncreaseFactor;
            maxHp = hp;

            goldDrop += (int)(waveNumber * goldDropIncreaseFactor);
        }

        public void SetSpawner(Spawner spawner){
            this.spawner = spawner;
        }

        public void RemoveHp(float towerDamage){
            hp -= towerDamage;

            if(hp <= 0){
                hp = 0;
                Player.Player.AddEnemyKilled();
                Player.Player.AddGold(goldDrop);
                spawner.DestroyEnemy(gameObject);
            }
        }

        public int GetDamage()
        {
            return damage;
        }

        public float GetHp(){
            return hp;
        }

        public float GetMaxHp(){
            return maxHp;
        }

        private void Update()
        {
            if (wayGeted)
            {
                if (Vector3.Distance(transform.position, way.wayPoints[pointIndex].position) >= Vector3.Distance(way.wayPoints[pointIndex + 1].position, way.wayPoints[pointIndex].position))
                {
                    pointIndex++;
                    transform.forward = (way.wayPoints[pointIndex + 1].position - way.wayPoints[pointIndex].position).normalized;
                }

                transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
            }
        }

    }
}
