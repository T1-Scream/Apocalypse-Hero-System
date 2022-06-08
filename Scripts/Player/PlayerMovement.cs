using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        public bool canMove;
        private SpriteRenderer spriteRender;
        private Rigidbody2D rb2D;
        private Vector2 movement;

        private Animator playerAni;
        string curState;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            playerAni = GetComponent<Animator>();
            spriteRender = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if (!canMove) return;

            rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);

            if (movement.x > 0) {
                PlayAnimation("WalkRight");
                spriteRender.flipX = false;
            }
            else if (movement.x < 0) {
                PlayAnimation("WalkRight");
                spriteRender.flipX = true;
            }
            else if (movement.y > 0)
                PlayAnimation("WalkUp");
            else if (movement.y < 0)
                PlayAnimation("WalkDown");
            else
                PlayAnimation("Idle");
        }

        public void PlayAnimation(string state)
        {
            if (curState == state) return;

            playerAni.Play(state);
            curState = state;
        }
    }
}
