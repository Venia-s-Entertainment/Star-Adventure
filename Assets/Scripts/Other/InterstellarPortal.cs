using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstellarPortal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject crystal;
    [SerializeField] SolarStormController solarStorm;
    [SerializeField] Rotator portalEnternace;
    [SerializeField] GameObject portalEffect;

    bool hasCrystal;

    void Update()
    {
        portalEffect.SetActive(hasCrystal && solarStorm.isGoing);
        portalEnternace.enabled = hasCrystal && solarStorm.isGoing;
    }
    public void Interact()
    {
        PlaceComponent(Inventory.itemList[0] != null);
    }
    private void PlaceComponent(bool v)
    {
        if (v)
        {
            Inventory.itemList[0].DropAll();

            Instantiate(crystal, transform.position, Quaternion.identity, transform);

            hasCrystal = true;
        }
    }
}
