using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] GameObject blastParticles;

    Rigidbody rb;
    HealthManager hm;
    ShipStats shipStats;

    private void Start()
    {
        hm = FindObjectOfType<HealthManager>();
        shipStats = FindObjectOfType<ShipStats>();
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShipParts"))
        {
            shipStats.TakeDamage((int)damage);
        }
        if (other.gameObject.layer == 11 && other.GetComponent<PlayerController>())
        {
            hm.TakeDamage(damage);
        }
        if (other.gameObject.layer != 2)
        {
            Instantiate(blastParticles, transform.position, Quaternion.identity, transform.parent);

            Destroy(gameObject);
        }
    }
}
