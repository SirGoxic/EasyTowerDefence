using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private Button quitButton = null;

        [SerializeField]
        private Text goldText = null;
        [SerializeField]
        private Text hpText = null;
        [SerializeField]
        private Text waveText = null;

        [SerializeField]
        private Enemy.Spawner spawner = null;

        private void Start(){
            quitButton.onClick.AddListener(Player.Player.QuitGame);
        }

        private void Update()
        {
            goldText.text = Player.Player.GetGold().ToString();

            hpText.text = Player.Player.GetHp().ToString();

            waveText.text = spawner.GetWaveCount().ToString();
        }
    }
}
