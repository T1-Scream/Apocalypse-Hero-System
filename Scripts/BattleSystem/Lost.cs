using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public class Lost : State
    {
        public Lost(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            BattleSystem.Player.ShowStateMessage("¥¢±Ñ", Color.red);
            BattleSystem.Enemy.ShowStateMessage("³Ó§Q", Color.yellow);
            BattleSystem.closeBtn.gameObject.SetActive(true);
        }
    }
}