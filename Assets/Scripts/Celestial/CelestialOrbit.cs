using Settings;
using UnityEngine;

public class CelestialOrbit : MonoBehaviour
{
    public float orbitSpeed;
    [SerializeField] Transform orbitParent;

    [System.Obsolete]
    void Update()
    {
        transform.RotateAround(orbitParent.position, Vector3.up, orbitSpeed * GameSettings.SimulationSpeed * Time.deltaTime);
    }
}
