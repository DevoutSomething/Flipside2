using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTrigger : MonoBehaviour
{
    public Barrel barrel;
    private bool hasActivated;
    private void Start()
    {
        barrel = gameObject.GetComponentInChildren<Barrel>();
        hasActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "player" && !hasActivated)
        {
            barrel.runBarrel();
            hasActivated = true;
        }
    }
}
