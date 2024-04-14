using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float Speed;
    void Update()
    {
        transform.Rotate(0, Speed * GameSettings.SimulationSpeed * Time.deltaTime, 0);
    }
}
