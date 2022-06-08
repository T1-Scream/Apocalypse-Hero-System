using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Audio.Sound;
using Shoping.Buy;

namespace Turnbased.Combat
{
    public class Fighter : MonoBehaviour
    {
        public static Fighter instance;

        public string fighterName;
        public Sprite fighterSprite;
        public Image fighterImage;
        public int damage;
        public int maxHealth;
        public int curHealth;
        public int coin;
        public int dropCoin;
        public Text nameText;
        public Text healthText;
        public Text attackText;
        public Text coinText;
        public Text stateMsg;
        public Vector3 imgScaleSize;
        public Color color;
        public bool finalFigher;

        public bool TakeDamage(int damage)
        {
            curHealth -= damage;
            FindObjectOfType<AudioManager>().PlayLoopFasting("hurt", 0f);

            if (curHealth <= 0) {
                curHealth = 0;
                return true;
            }
            else
                return false;
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetHP(int hp)
        {
            healthText.text = "¦å¶q : " + hp;
        }

        public void SetAtk(int attack)
        {
            attackText.text = "¶Ë®` : " + attack;
        }

        public void GetHP(int hp)
        {
            maxHealth += hp;
        }

        public void GetAtk(int attack)
        {
            damage += attack;
        }

        public void GetCoins(int count)
        {
            coin += count;
        }

        public void TotalCoins()
        {
            coinText.text = ": " + coin;
        }

        public void ShowStateMessage(string message, Color color)
        {
            stateMsg.color = color;
            stateMsg.text = message;
        }

        public void GetItem(Item.ItemType itemType)
        {
            switch (itemType) {
                default:
                case Item.ItemType.Atk_1: GetAtk(1); break;
                case Item.ItemType.Atk_5: GetAtk(5); break;
                case Item.ItemType.Atk_10: GetAtk(10); break;
                case Item.ItemType.Atk_100: GetAtk(100); break;
                case Item.ItemType.HP_1: GetHP(1); break;
                case Item.ItemType.HP_5: GetHP(5); break;
                case Item.ItemType.HP_10: GetHP(10); break;
                case Item.ItemType.HP_100: GetHP(100); break;
            }
        }
    }
}
