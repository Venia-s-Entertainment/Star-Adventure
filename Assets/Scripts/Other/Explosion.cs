using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    [SerializeField] float deathDistance = 5;
    [SerializeField] float maxForce = 2500;
    [SerializeField] int shockWaveSpeed = 5;
    [SerializeField] float shockWaveLifeTime = 0.3f;
    [SerializeField] float destroyTime = 5;

    HealthManager hm;
    // Start is called before the first frame update
    void Start()
    {
        hm = FindObjectOfType<HealthManager>();
        transform.localScale = Vector3.one;

        StartCoroutine(IntializeShockWave());

        Destroy(transform.parent.gameObject, destroyTime);
    }

    // Update is called once per frame
    IEnumerator IntializeShockWave()
    {
        while (shockWaveLifeTime > 0)
        {
            shockWaveLifeTime -= Time.deltaTime;

            transform.localScale += Vector3.one * shockWaveSpeed * Time.deltaTime;
            maxForce -= 25;

            yield return new WaitForSeconds(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            Vector3 diraction = (other.transform.position - transform.position).normalized;
            float F = maxForce - Vector3.Distance(transform.position, other.transform.position);

            if (F > 0) rb.AddForce(diraction * F, ForceMode.Impulse);
        }
        if (other.GetComponent<PlayerController>())
        {
            if (Vector3.Distance(other.transform.position, transform.position) < deathDistance)
            {
                hm.TakeDamage(100);
                return;
            }

            float explosionDamage = Settings.PlayerSettings.maxHealth - Vector3.Distance(transform.position, other.transform.position) * 1.5f;
            explosionDamage = Mathf.Clamp(explosionDamage, 0, Settings.PlayerSettings.maxHealth);

            hm.TakeDamage(Mathf.RoundToInt(explosionDamage));
        }
    }
}
