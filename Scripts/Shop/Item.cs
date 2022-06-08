using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shoping.Buy
{
    public class Item
    {
        public enum ItemType {
            Atk_1,
            Atk_5,
            Atk_10,
            Atk_100,
            HP_1,
            HP_5,
            HP_10,
            HP_100
        }

        public static int GetCost(ItemType itemType)
        {
            switch (itemType) {
                default:
                case ItemType.Atk_1: return 1;
                case ItemType.Atk_5: return 5;
                case ItemType.Atk_10: return 10;
                case ItemType.Atk_100: return 100;
                case ItemType.HP_1: return 1;
                case ItemType.HP_5: return 5;
                case ItemType.HP_10: return 10;
                case ItemType.HP_100: return 100;
            }
        }

        public static Sprite GetSprite(ItemType itemType)
        {
            switch (itemType) {
                default:
                case ItemType.Atk_1: return ItemInfo.instance.item_atk1;
                case ItemType.Atk_5: return ItemInfo.instance.item_atk5;
                case ItemType.Atk_10: return ItemInfo.instance.item_atk10;
                case ItemType.Atk_100: return ItemInfo.instance.item_atk100;
                case ItemType.HP_1: return ItemInfo.instance.item_hp1;
                case ItemType.HP_5: return ItemInfo.instance.item_hp5;
                case ItemType.HP_10: return ItemInfo.instance.item_hp10;
                case ItemType.HP_100: return ItemInfo.instance.item_hp100;
            }
        }
    }
}
