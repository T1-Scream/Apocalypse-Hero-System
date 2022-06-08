using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping.Buy
{
    public class ItemInfo : MonoBehaviour
    {

        private static ItemInfo _instance;
        public static ItemInfo instance {
            get { if (_instance == null) _instance = Instantiate(Resources.Load<ItemInfo>("ItemInfo")); return _instance; }
        }

        public Sprite item_atk1;
        public Sprite item_atk5;
        public Sprite item_atk10;
        public Sprite item_atk100;
        public Sprite item_hp1;
        public Sprite item_hp5;
        public Sprite item_hp10;
        public Sprite item_hp100;
    }
}
