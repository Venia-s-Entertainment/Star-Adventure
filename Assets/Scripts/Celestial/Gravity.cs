using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] CelestialInfo info;

    Rigidbody[] rb;
    private void Start()
    {
        UpdateAllPhysicalObjects();
    }

    public void UpdateAllPhysicalObjects()
    {
        rb = FindObjectsOfType<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody physicalObject in rb)
        {
            if (physicalObject.gameObject.layer != 12)
            {
                Vector3 velocity = (transform.position - physicalObject.position).normalized;

                float distance = Vector3.Distance(transform.position, physicalObject.position);
                float F = (info.Mass * physicalObject.mass) / (distance * distance);

                physicalObject.AddForce(velocity * F);
            }
        }
    }
}
