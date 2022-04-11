using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public InventoryView inventoryView;
    public GameObject slotButton;

    private void Start()
    {
        inventoryView = FindObjectOfType<InventoryView>();
        Debug.Log(inventoryView.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
    }
}
