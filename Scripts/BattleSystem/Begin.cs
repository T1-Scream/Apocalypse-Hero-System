using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turnbased.Combat
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //Set name
            BattleSystem.Player.SetName(BattleSystem.Player.fighterName);
            BattleSystem.Enemy.SetName(BattleSystem.Enemy.fighterName);
            //Set enemy sprite
            BattleSystem.Enemy.fighterImage.sprite = BattleSystem.Enemy.fighterSprite;
            //Set enemy sprite scale
            BattleSystem.Enemy.fighterImage.rectTransform.localScale = BattleSystem.Enemy.imgScaleSize;
            //Set color
            BattleSystem.Enemy.fighterImage.color = BattleSystem.Enemy.color;
            //Set health
            BattleSystem.Player.curHealth = BattleSystem.Player.maxHealth;
            BattleSystem.Enemy.curHealth = BattleSystem.Enemy.maxHealth;
            BattleSystem.Player.SetHP(BattleSystem.Player.curHealth);
            BattleSystem.Enemy.SetHP(BattleSystem.Enemy.curHealth);
            //Set attack
            BattleSystem.Player.SetAtk(BattleSystem.Player.damage);
            BattleSystem.Enemy.SetAtk(BattleSystem.Enemy.damage);
            //Win lost text
            BattleSystem.Player.ShowStateMessage("", Color.white);
            BattleSystem.Enemy.ShowStateMessage("", Color.white);

            BattleSystem.restartBtn.gameObject.SetActive(false);
            BattleSystem.closeBtn.gameObject.SetActive(false);
            BattleSystem.getCoinText.gameObject.SetActive(false);
            BattleSystem.end = false;

            yield return new WaitForSeconds(0.1f);
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}
