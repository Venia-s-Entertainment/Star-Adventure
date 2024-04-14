using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePortal : MonoBehaviour
{
    [SerializeField] Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.transform.position = teleportLocation.position;

            PlayerStats.celestial = null;
        }
    }
}
