using UnityEngine;
using Settings;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    private float rollSpeed;
    private Rigidbody ship;

    public bool isControlling;
    public bool isGrounded;

    public MapController map;
    [SerializeField] float Acceleration;
    [SerializeField] float torque;
    [SerializeField] AudioSource Engines;
    [SerializeField] WarpEngine warpEngine;

    [Header("Piloting ship")]
    [SerializeField] Transform pilotPlace;
    [SerializeField] Transform exitPlace;
    [SerializeField] GameObject playerHud;
    [SerializeField] GameObject shipHud;

    PlayerController pl;
    // Start is called before the first frame update
    void Start()
    {
        rollSpeed = PlayerSettings.Sensivity / 2;

        //ssc = FindObjectOfType<SpaceShipController>();
        ship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isControlling && !map.mapIsEnabled)
        {
            if (!warpEngine.warpStarted)
            {
                float AxisX = Input.GetAxis("Mouse X");
                float AxisY = Input.GetAxis("Mouse Y");
                float roll = Input.GetAxis("Roll");

                ship.AddTorque(transform.right * -AxisY * torque * PlayerSettings.Sensivity * Time.fixedDeltaTime);
                ship.AddTorque(transform.up * AxisX * torque * PlayerSettings.Sensivity * Time.fixedDeltaTime);
                ship.AddTorque(transform.forward * roll * torque * rollSpeed * Time.fixedDeltaTime);

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float fly = Input.GetAxis("Flying");

                ship.AddForce(transform.up * fly * Acceleration, 0);
                ship.AddForce(transform.right * horizontal * Acceleration, 0);
                ship.AddForce(transform.forward * vertical * Acceleration, 0);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer != 11)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer != 11)
        {
            isGrounded = false;
        }
    }
    public void TogglePiloting()
    {
        if (!warpEngine.warpStarted)
        {
            isControlling = !isControlling;

            if (isControlling)
            {
                Engines.Play();
                pl = FindObjectOfType<PlayerController>();
                pl.GetComponent<Rigidbody>().velocity = Vector3.zero;
                PlayerStats.inWater = false;
                pl.cam.parent = pilotPlace;
                pl.cam.localPosition = Vector3.zero;
                pl.GetComponent<Animator>().CrossFade("Idle", 0);
                pl.isWalking = false;
                pl.isGrounded = false;
                pl.GetComponent<Rigidbody>().drag = 0;
                gameObject.tag = "Player";
            }
            else
            {
                Engines.Stop();
                pl.GetComponent<Rigidbody>().velocity = ship.velocity;
                pl.cam.parent = pl.transform;
                pl.cam.localPosition = pl.camLocalPosition;
                pl.transform.position = exitPlace.position;
                gameObject.tag = "Ship";
            }

            map.OpenMap(false);

            pl.transform.localEulerAngles = Vector3.zero;

            PlayerStats.isPiloting = isControlling;
            pl.cam.localRotation = Quaternion.identity;

            shipHud.SetActive(isControlling);
            playerHud.SetActive(!isControlling);

            pl.GetComponent<Animator>().Update(0);
            pl.gameObject.SetActive(!isControlling);
        }
    }    
}
