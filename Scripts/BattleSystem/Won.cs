using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public class Won : State
    {
        public Won(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            BattleSystem.Player.GetCoins(BattleSystem.Enemy.dropCoin);
            BattleSystem.getCoinText.gameObject.SetActive(true);
            BattleSystem.getCoinText.text = $"獲得 {BattleSystem.Enemy.dropCoin} 幣 !";
            BattleSystem.Player.TotalCoins();

            BattleSystem.Player.ShowStateMessage("勝利", Color.yellow);
            BattleSystem.Enemy.ShowStateMessage("失敗", Color.red);
            BattleSystem.closeBtn.gameObject.SetActive(true);
            BattleSystem.restartBtn.gameObject.SetActive(true);
            BattleSystem.end = true;

            if (BattleSystem.Enemy.finalFigher) {
                BattleSystem.clearSprite.SetActive(true);
                BattleSystem.clearCollider.isTrigger = true;
            }
        }
    }
}
