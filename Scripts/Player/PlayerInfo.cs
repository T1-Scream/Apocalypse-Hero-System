using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Turnbased.Combat;

namespace Player.Movement {
    public class PlayerInfo : MonoBehaviour
    {
        public GameObject info;
        public Text playerHealth;
        public Text playerDamage;
        public Text playerCoin;

        private Fighter player;
        private bool infoOpen;
        public bool _infoOpen => infoOpen;

        private void Awake()
        {
            player = GetComponent<Fighter>();
        }

        private void Update()
        {
            //get info
            playerHealth.text = "¦å¶q : " + player.maxHealth.ToString();
            playerDamage.text = "¶Ë®` : " + player.damage.ToString();
            playerCoin.text = "ª÷¹ô : " + player.coin.ToString();

            if (Input.GetKeyDown(KeyCode.M)) infoOpen = !infoOpen;
        }

        private void FixedUpdate()
        {
            if (_infoOpen)
                info.SetActive(true);
            else
                info.SetActive(false);
        }
    }
}
