using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSurfaceLoader : MonoBehaviour
{
    [SerializeField] CelestialInfo celestial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);

                celestial.gravity?.UpdateAllPhysicalObjects();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
