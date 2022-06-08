using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public abstract class BattleStateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
            StartCoroutine(State.Start());
        }
    }
}
