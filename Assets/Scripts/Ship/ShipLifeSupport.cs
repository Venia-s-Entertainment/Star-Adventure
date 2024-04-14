using UnityEngine;

public class ShipLifeSupport : MonoBehaviour
{
    PlayerController p;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            p = other.GetComponent<PlayerController>();

            if (!PlayerStats.onShip)
            {
                PlayerStats.onShip = true;

                p.transform.parent = transform;

                PlayerStats.Temperature = 25;
                PlayerStats.canBreath = !PlayerStats.underWater;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerStats.onShip = false;

            if (!PlayerStats.celestial)
            {
                if (!PlayerStats.celestial.gravity)
                    p.transform.parent = null;

                PlayerStats.Temperature = Space.Temp;
                PlayerStats.canBreath = Space.canBreath;
            }
            else
            {
                CelestialInfo c = PlayerStats.celestial;

                p.transform.parent = c.transform;
                PlayerStats.canBreath = !PlayerStats.underWater && c.isHabitable;
            }
        }
    }
}
