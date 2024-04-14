using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] float waterDensity = 4;
    [SerializeField] ParticleSystem underWaterEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().drag = waterDensity;
        }
        if (other.CompareTag("Player"))
        {
            PlayerStats.inWater = true;
        }
        if (other.CompareTag("Head") || (PlayerStats.isPiloting && other.CompareTag("Player")))
        {
            PlayerStats.underWater = true;
            PlayerStats.canBreath = false;

            underWaterEffect.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().drag = 0;
        }
        if (other.CompareTag("Player"))
        {
            PlayerStats.inWater = false;
        }
        if (other.CompareTag("Head") || (PlayerStats.isPiloting && other.CompareTag("Player")))
        {
            PlayerStats.underWater = false;
            PlayerStats.canBreath = PlayerStats.onShip || PlayerStats.celestial.isHabitable;

            underWaterEffect.Stop();
        }
    }
}
