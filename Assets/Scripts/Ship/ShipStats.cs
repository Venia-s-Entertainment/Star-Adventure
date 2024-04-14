using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipStats : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] Text speedText;

    [Header("Engines")]
    [SerializeField] AudioSource engineAudio;
    [SerializeField] WarpEngine warp;

    [Header("Ship Health")]
    [SerializeField] GameObject destructioStage1;
    [SerializeField] GameObject destructioStage2;
    [SerializeField] AudioSource crashSound;
    [SerializeField] Text healthText;
    [SerializeField] GameObject Explosion;
    [SerializeField] float maxVelocity = 25;
    [SerializeField] int maxHealth = 350;

    Rigidbody ship;
    SpaceShipController shipController;
    private GameObject[] modules;
    bool isDestroyed = false;

    Gravity[] celestials;
    private void Start()
    {
        healthText.text = $"Health: {maxHealth}";
        modules = GameObject.FindGameObjectsWithTag("Ship");

        ship = GetComponent<Rigidbody>();
        shipController = GetComponent<SpaceShipController>();
        celestials = FindObjectsOfType<Gravity>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && shipController.isControlling)
        {
            warp.StartWarp();
        }

        engineAudio.pitch = (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Flying") != 0) && !shipController.map.mapIsEnabled? 2 : 0.8f;
        speedText.text = $"Velocity: {Mathf.RoundToInt(ship.velocity.magnitude)}u/s";
    }
    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        healthText.text = $"Health: {maxHealth}";

        if (maxHealth <= 500 && !isDestroyed)
        {
            destructioStage1.SetActive(true);
        }
        if (maxHealth <= 100 && !isDestroyed)
        {
            destructioStage1.SetActive(false);
            destructioStage2.SetActive(true);
        }
        if (maxHealth <= 0 && !isDestroyed)
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        warp.EndWarp();
        destructioStage2.SetActive(false);

        if (shipController.isControlling)
        {
            shipController.TogglePiloting();
        }

        foreach (GameObject module in modules)
        {
            module.transform.parent = transform.parent;
            module.AddComponent<Rigidbody>();
        }
        foreach (Gravity c in celestials)
        {
            c.UpdateAllPhysicalObjects();
        }

        Instantiate(Explosion, transform.position, Quaternion.identity, transform.parent);

        enabled = false;
        shipController.enabled = false;
        isDestroyed = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        warp.EndWarp();

        if (collision.relativeVelocity.magnitude > maxVelocity)
        {
            TakeDamage(Mathf.RoundToInt(collision.relativeVelocity.magnitude - maxVelocity));

            crashSound.PlayOneShot(crashSound.clip);
        }
    }
}
