using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LinkStore : MonoBehaviour
{
    public PlayerController playerLink;
    public HealthController HealthController;
    public CameraController camera;
    public CinemachineVirtualCamera cvCamera;
    public Spawner spawner;
    public InventorySystemTest InventorySystemTest;
    public Joystick joystick;
    public Button fireWeapon;
    public Button reloadWeapon;
    public List<GameObject> weapons;
    public List<Item> inventoryItems;
    public List<GameObject> heal;
    public static LinkStore Instans;
    

    private void Awake()
    {
        Instans = this;
    }
}
