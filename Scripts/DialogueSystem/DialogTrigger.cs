using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player.Movement;

namespace Dialogue
{
    public class DialogTrigger : MonoBehaviour
    {
        public Dialog dialog;

        private bool complete;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!complete) {
                FindObjectOfType<PlayerMovement>().canMove = false;
                FindObjectOfType<PlayerMovement>().PlayAnimation("Idle");
                dialog.gameObject.SetActive(true);
                dialog.dialogBox.SetActive(true);
                complete = true;
            }
        }
    }
}
