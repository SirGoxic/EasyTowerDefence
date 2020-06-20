using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject levelUpPanel = null;
        [SerializeField]
        private GameObject deathPanel = null;

        public void VisibilityLevelUpPanel(bool visibility){
            levelUpPanel.SetActive(visibility);
        }

        public void ChangeTowerInfo(Tower.Tower tower){
            levelUpPanel.GetComponent<LevelUp>().TowerInfo(tower);
        }

        public void VisibilityDeathPanel(bool visibility){
            deathPanel.SetActive(visibility);
        }
    }
}
