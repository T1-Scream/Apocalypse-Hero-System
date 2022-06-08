using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Movement;
using Turnbased.Combat;

public class Battle : MonoBehaviour
{
    [HideInInspector] public bool start;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Fighter fighter;

    private GameObject dialog;

    private void Start()
    {
        dialog = battleSystem.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!start)
            StartBattle();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        start = false;
    }

    public void StartBattle()
    {
        player.canMove = false;
        battleSystem.Enemy = fighter;
        dialog.SetActive(true);
        start = true;
    }
}
