using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Movement;

public class MoveToNextEnvironment : MonoBehaviour
{
    public Transform teleportPoint;
    public Camera mainCamera;
    public Camera playerCamera;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        player.transform.position = teleportPoint.position;
        mainCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }
}
