using System;
using Cinemachine;
using UnityEngine;

public class LinkStore : MonoBehaviour
{
    public PlayerController playerLink;
    public HealthController HealthController;
    public CameraController camera;
    public CinemachineVirtualCamera cvCamera;
    public Spawner spawner;
    public static LinkStore Instans;

    private void Start()
    {
        Instans = this;
    }
}
