using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost
    }
}
