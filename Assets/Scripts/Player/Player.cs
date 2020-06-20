using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Player
{
    public static class Player
    {
        private static int hp;
        private static int gold;
        private static int enemyKilled;

        public static int GetGold(){
            return gold;
        } 

        public static void AddGold(int sum){
            gold += sum;
        }

        public static void SetGold(int sum){
            gold = sum;
        }

        public static void RemoveGold(int sum){
            gold -= sum;
        }

        public static void SetHp(int startHp){
            hp = startHp;
        }

        public static int GetHp(){
            return hp;
        }

        public static void RemoveHp(int damage){
            if(hp - damage < 0){
                hp = 0;
            }else{
                hp -= damage;
            }
        }

        public static void SetEnemyKilled(int killed){
            enemyKilled = killed;
        }

        public static int GetEnemyKilled(){
            return enemyKilled;
        }

        public static void AddEnemyKilled(){
            enemyKilled++;
        }

        public static void QuitGame(){
            Application.Quit();
        }
    }
}
