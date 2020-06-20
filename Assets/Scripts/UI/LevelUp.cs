using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class LevelUp : MonoBehaviour
    {
        [SerializeField]
        private Button levelUpBtn = null;

        [SerializeField]
        private Text shootSpeedText = null;
        [SerializeField]
        private Text damageText = null;
        [SerializeField]
        private Text priceText = null;
        [SerializeField]
        private Text levelText = null;

        public void TowerInfo(Tower.Tower tower){
            levelUpBtn.onClick.RemoveAllListeners();
            levelUpBtn.onClick.AddListener(() => TowerLevelUp(tower));
            UpdateTowerInfo(tower);
        }

        private void UpdateTowerInfo(Tower.Tower tower){
            shootSpeedText.text = tower.GetShootSpeed().ToString("0.00");
            damageText.text = tower.GetDamage().ToString("0.00");
            priceText.text = tower.GetLevelPrice().ToString();
            levelText.text = tower.GetLevel().ToString();
        }

        private void TowerLevelUp(Tower.Tower tower){
            if(tower.LevelUp()){
                Debug.Log("Level up successful");
            }else{
                Debug.Log("Not enough gold");
            }
            UpdateTowerInfo(tower);
        }
    }
}
