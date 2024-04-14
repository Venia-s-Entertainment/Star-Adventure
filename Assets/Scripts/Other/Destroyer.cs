using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Destroyer : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            healthManager.TakeDamage(100);
        }
        if (other.GetComponent<ShipStats>())
        {
            other.GetComponent<ShipStats>().DestroyShip();
        }
    }
}
