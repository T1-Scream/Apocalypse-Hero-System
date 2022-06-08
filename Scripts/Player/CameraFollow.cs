using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smooth = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 playerPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smooth);
    }
}
