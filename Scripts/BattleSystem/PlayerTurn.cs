using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);

            bool isDead = BattleSystem.Enemy.TakeDamage(BattleSystem.Player.damage);
            BattleSystem.Enemy.SetHP(BattleSystem.Enemy.curHealth);

            if (isDead)
                BattleSystem.SetState(new Won(BattleSystem));
            else
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}