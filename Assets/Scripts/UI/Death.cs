using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private Player.GameInic gameInic = null;

        [SerializeField]
        private Text enemyKilled = null;

        [SerializeField]
        private Button restartBtn = null;
        [SerializeField]
        private Button quitBtn = null;

        private void Start()
        {
            restartBtn.onClick.AddListener(gameInic.Restart);
            quitBtn.onClick.AddListener(Player.Player.QuitGame);
        }

        private void OnEnable()
        {
            enemyKilled.text = Player.Player.GetEnemyKilled().ToString();
        }
    }
}
