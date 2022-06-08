using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player.Movement;

namespace Turnbased.Combat
{
    public class BattleSystem : BattleStateMachine
    {
        private BattleState state;
        private bool _startAuto;

        public Fighter Player;
        public Fighter Enemy;
        public Button restartBtn;
        public Button closeBtn;
        public Text getCoinText;
        public Toggle autoToggle;
        public GameObject clearSprite;
        public BoxCollider2D clearCollider;
        public PlayerMovement playerMovement;

        internal bool startAuto { get { return _startAuto; }
            set { _startAuto = value;
                if (_startAuto == true)
                    StartCoroutine(StartAuto());
                else
                    StopCoroutine(StartAuto());
            }
        }
        internal bool end;
        protected GameObject dialog;

        private void OnEnable()
        {
            dialog = gameObject;
            playerMovement.PlayAnimation("Idle");
            state = BattleState.Start;
            SetState(new Begin(this));
        }

        public void StartBattle()
        {
            state = BattleState.Start;
            SetState(new Begin(this));
        }

        public void Auto()
        {
            if (autoToggle.isOn)
                startAuto = true;
            else
                startAuto = false;
        }

        public void CloseDialog()
        {
            StopAllCoroutines();
            closeBtn.gameObject.SetActive(false);
            getCoinText.gameObject.SetActive(false);
            dialog.SetActive(false);
            playerMovement.canMove = true;
        }

        IEnumerator StartAuto()
        {
            while (true) {
                yield return new WaitForSeconds(1f);
                if (autoToggle.isOn)
                    if (end)
                        StartBattle();
                yield return null;
            }
        }
    }
}
