using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);

            bool isDead = BattleSystem.Player.TakeDamage(BattleSystem.Enemy.damage);
            BattleSystem.Player.SetHP(BattleSystem.Player.curHealth);

            if (isDead)
                BattleSystem.SetState(new Lost(BattleSystem));
            else
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}
