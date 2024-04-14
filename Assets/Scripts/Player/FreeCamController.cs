using Settings;
using UnityEngine;
using UnityEngine.UI;

public class FreeCamController : MonoBehaviour
{
    private float Speed = 1f;
    private float RollSpeed = 0.5f;
    [SerializeField] float Sensivity;
    [SerializeField] PlayerController player;

    [Header("HUD")]
    [SerializeField] GameObject nclpHUD;
    [SerializeField] Text nclipSpeed;
    [SerializeField] Text simSpeed;

    private float strength;
    // Update is called once per frame
    void Update()
    {
        float AxisY = Input.GetAxis("Mouse Y");
        float AxisX = Input.GetAxis("Mouse X");
        float AxisZ = Input.GetAxis("Roll");
        AxisY = Mathf.Clamp(AxisY, -90, 90);

        transform.Rotate(new Vector3(-AxisY * Sensivity, AxisX * Sensivity, AxisZ * RollSpeed));

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
            GameSettings.SimulationSpeed *= 2;

        if (Input.GetKeyDown(KeyCode.Minus))
            GameSettings.SimulationSpeed /= 2;


        if (Input.mouseScrollDelta.y != 0)
        {
            strength = Speed * 0.5f;

            Speed += Input.mouseScrollDelta.y * strength;
            Speed = Mathf.Clamp(Speed, 0.01f, 50000);
        }

        transform.Translate(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime);

        nclipSpeed.text = $"Your speed: {Speed}u/s";
        simSpeed.text = $"Simulation speed: {GameSettings.SimulationSpeed}";
    }
    public void ToggleNoclip(bool enabled)
    {
        this.enabled = enabled;

        transform.parent = enabled ? null : player.transform;
        transform.localPosition = enabled ? transform.localPosition : player.camLocalPosition;
        transform.rotation = enabled ? transform.rotation : Quaternion.identity;

        nclpHUD.SetActive(enabled);
        player.gameObject.SetActive(!enabled);

        PlayerSettings.noclipEnabled = enabled;
    }
}
