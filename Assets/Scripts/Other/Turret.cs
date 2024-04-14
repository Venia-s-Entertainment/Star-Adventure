using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] LayerMask ingnore;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioSource shot;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float recoliTime = 0.2f;
    [SerializeField] float maxDistance = 200;
    [SerializeField] string targetTag;

    [SerializeField] Transform target;
    RaycastHit hit;
    float defualtTime;
    private void Start()
    {
        StartCoroutine(RotateToTarget(target));

        defualtTime = recoliTime;
    }
    private void FixedUpdate()
    {
        Ray ray = new Ray(shotPoint.position, shotPoint.forward);

        if (Physics.Raycast(ray, out hit, maxDistance, ingnore))
        {
            if ((hit.collider.CompareTag("Player") || hit.collider.CompareTag("ShipParts")) && recoliTime <= 0)
            {
                Shot();
            }
        }
    }
    private void Update()
    {
        recoliTime -= Time.deltaTime;
    }
    private void Shot()
    {
        shot.PlayOneShot(shot.clip);

        Instantiate(bullet, shotPoint.position, shotPoint.rotation, transform.parent);

        recoliTime = defualtTime;
    }

    IEnumerator RotateToTarget(Transform target)
    {
        this.target = target;

        while (true)
        {
            if (Vector3.Distance(target.position, transform.position) < maxDistance)
            {
                Vector3 rotationPos = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(rotationPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }

            yield return new WaitForSeconds(0);
        }
    }
}
