using System;
using UnityEngine;

public class LinkStore : MonoBehaviour
{
    public PlayerController playerLink;
    public HealthController HealthController;
    public Spawner spawner;
    public static LinkStore Instans;

    private void Start()
    {
        Instans = this;
    }
}
