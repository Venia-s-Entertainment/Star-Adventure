using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
    public bool IsActivated;
    [SerializeField] Camera mainCam;
    [SerializeField] float power;

    [Header("HUD")]
    [SerializeField] GameObject hud;
    [SerializeField] Text percent;
    public Slider fuelBar;

    Rigidbody rb;
    PlayerController pl;
    private void Update()
    {
        ChangeBarValue();
    }
    public void ChangeBarValue()
    {
        fuelBar.value -= Input.GetAxis("Vertical") != 0 && !pl.isGrounded ? 0.3f * Time.deltaTime : 0;
        fuelBar.value -= Input.GetAxis("Horizontal") != 0 && !pl.isGrounded ? 0.3f * Time.deltaTime : 0;
        fuelBar.value -= Input.GetAxis("Flying") != 0 && !pl.isGrounded ? 0.3f * Time.deltaTime : 0;

        percent.text = Mathf.RoundToInt(fuelBar.value).ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuelBar.value > 0 && pl.enabled)
        {
            rb.AddForce(pl.cam.transform.forward * Input.GetAxis("Vertical") * power * Time.fixedDeltaTime);
            rb.AddForce(pl.cam.transform.right * Input.GetAxis("Horizontal") * power * Time.fixedDeltaTime);

            if (PlayerStats.celestial?.gravity != null)
                rb.AddForce(pl.cam.transform.up * Input.GetAxis("Flying") * power * 3 * Time.fixedDeltaTime);
            else
                rb.AddForce(pl.cam.transform.up * Input.GetAxis("Flying") * power * Time.fixedDeltaTime);

            if (PlayerStats.celestial == null || PlayerStats.celestial.gravity == null)
            {
                transform.Rotate(0, 0, Input.GetAxis("Roll") * PlayerSettings.Sensivity);
                transform.Rotate(-Input.GetAxis("Mouse Y") * PlayerSettings.Sensivity, 0, 0);
                transform.Rotate(0, Input.GetAxis("Mouse X") * PlayerSettings.Sensivity, 0);
            }
        }
    }
    public void ToggleJetPack(bool IsActivated)
    {
        this.IsActivated = IsActivated;

        rb = GetComponent<Rigidbody>();
        pl = GetComponent<PlayerController>();

        enabled = IsActivated;
        rb.drag = !PlayerStats.inWater ? 0 : rb.drag;
    }
}
