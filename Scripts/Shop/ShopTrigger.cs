using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shoping.Buy;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shop.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shop.SetActive(false);
    }
}
