using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Turnbased.Combat;
using Audio.Sound;

namespace Shoping.Buy
{
    public class Shop : MonoBehaviour
    {
        private Transform container;
        private Transform shopItem;

        public Fighter player;

        private void Awake()
        {
            container = transform.Find("Container");
            shopItem = container.Find("Item");
        }

        private void Start()
        {
            CreateItem(Item.ItemType.Atk_1, Item.GetSprite(Item.ItemType.Atk_1), "¶Ë®` 1", Item.GetCost(Item.ItemType.Atk_1), 0, 0);
            CreateItem(Item.ItemType.Atk_5, Item.GetSprite(Item.ItemType.Atk_5), "¶Ë®` 5", Item.GetCost(Item.ItemType.Atk_5), 100, 0);
            CreateItem(Item.ItemType.Atk_10, Item.GetSprite(Item.ItemType.Atk_10), "¶Ë®` 10", Item.GetCost(Item.ItemType.Atk_10), 200, 0);
            CreateItem(Item.ItemType.Atk_100, Item.GetSprite(Item.ItemType.Atk_100), "¶Ë®` 100", Item.GetCost(Item.ItemType.Atk_100), 300, 0);
            CreateItem(Item.ItemType.HP_1 ,Item.GetSprite(Item.ItemType.HP_1), "¦å¶q 1", Item.GetCost(Item.ItemType.HP_1), 0, 120);
            CreateItem(Item.ItemType.HP_5, Item.GetSprite(Item.ItemType.HP_5), "¦å¶q 5", Item.GetCost(Item.ItemType.HP_5), 100, 120);
            CreateItem(Item.ItemType.HP_10, Item.GetSprite(Item.ItemType.HP_10), "¦å¶q 10", Item.GetCost(Item.ItemType.HP_10), 200, 120);
            CreateItem(Item.ItemType.HP_100, Item.GetSprite(Item.ItemType.HP_100), "¦å¶q 100", Item.GetCost(Item.ItemType.HP_100), 300, 120);
            shopItem.gameObject.SetActive(false);
        }

        private void CreateItem(Item.ItemType itemType ,Sprite itemSprite, string itemName, int itemCost, int itemWidth, int itemHeight)
        {
            Transform itemTrans = Instantiate(shopItem, container);
            RectTransform itemRectTrans = itemTrans.GetComponent<RectTransform>();

            itemRectTrans.anchoredPosition = new Vector2(itemWidth, -itemHeight);

            itemTrans.Find("ItemImage").GetComponent<Image>().sprite = itemSprite;
            itemTrans.Find("ItemText").GetComponent<Text>().text = itemName;
            itemTrans.Find("Price").GetComponent<Text>().text = itemCost.ToString();
            itemTrans.Find("BuyBtn").GetComponent<Button>().onClick.AddListener(() => {
                BuyItem(itemType);
            });
        }

        private void BuyItem(Item.ItemType itemType)
        {
            if (SpendCoinToBuy(Item.GetCost(itemType))) {
                player.GetItem(itemType);
                FindObjectOfType<AudioManager>().PlayLoopFasting("coin", 0f);
            }
            else
                FindObjectOfType<AudioManager>().Play("error", 0f);
        }

        private bool SpendCoinToBuy(int itemPrice)
        {
            if (player.coin >= itemPrice) {
                player.coin -= itemPrice;
                player.TotalCoins();
                return true;
            }
            else
                return false;
        }
    }
}
