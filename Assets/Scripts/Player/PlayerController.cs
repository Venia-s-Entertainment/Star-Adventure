using UnityEngine;
using Settings;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerRaycast pr;
    public Transform cam;
    [SerializeField] HealthManager healthBar;

    private Rigidbody rb;
    private Animator player;

    public bool isWalking;
    private float MovementSpeed;

    public bool isRunning;
    private bool isPressed;
    public bool isGrounded;

    [Header("Damage Sounds")]
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;

    public Vector3 camLocalPosition;

    SpaceShipController shipController;
    float RotateY;
    private float CalculateSpeed()
    {
        float Force = 7.5f - rb.velocity.magnitude;

        if (rb.velocity.magnitude < 4f)  Force *= 200;          

        Force = Mathf.Clamp(Force, 0, PlayerSettings.characterStrength) * 1000;

        return Force;      
    }
    private void Start()
    {
        shipController = FindObjectOfType<SpaceShipController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        player = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        camLocalPosition = cam.localPosition;
    }
    private void Update()
    {
        if (!PlayerStats.inWater)
        {
            rb.drag = isWalking && PlayerStats.celestial ? 2 : 0;
        }

        RotateY -= Input.GetAxis("Mouse Y") * PlayerSettings.Sensivity;
        RotateY = Mathf.Clamp(RotateY, -60, 60);

        if (!GameSettings.isPaused)
        {
            if ((PlayerStats.celestial != null && PlayerStats.celestial.gravity != null) || !GetComponent<JetPack>().IsActivated)
            {
                cam.localEulerAngles = Vector3.right * RotateY;
                transform.Rotate(0, Input.GetAxis("Mouse X") * PlayerSettings.Sensivity, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !PlayerStats.inWater)
        {
            isPressed = true;
        }

        if (isWalking && PlayerStats.celestial)
            MovementSpeed = (PlayerSettings.characterSpeed + CalculateSpeed()) / 2;

        CheckInput();
        UpdateAnimation();
    }
    void FixedUpdate()
	{
        if (isGrounded || PlayerStats.inWater)
        {
            rb.AddForce(transform.forward * Input.GetAxis("Vertical") * (isRunning ? MovementSpeed * 4f : MovementSpeed) * Time.fixedDeltaTime);
            rb.AddForce(transform.right * Input.GetAxis("Horizontal") * (isRunning ? MovementSpeed * 4f : MovementSpeed) * Time.fixedDeltaTime);
        }

        //rb.AddForce(new Vector3(h * MovementSpeed, 0, v * MovementSpeed), 0);

        if (isPressed)
        {
            rb.AddForce(transform.up * PlayerSettings.jumpForce, ForceMode.Impulse);

            isPressed = false;
        }

        if (Input.GetKey(KeyCode.Space) && PlayerStats.inWater)
        {
            rb.AddForce(transform.up * 7 * PlayerSettings.jumpForce);
        }
        if (Input.GetKey(KeyCode.LeftControl) && PlayerStats.inWater)
        {
            rb.AddForce(-transform.up * 7 * PlayerSettings.jumpForce);
        }
    }
    public void UpdateAnimation()
    {
        player.SetBool("startRun", isGrounded && isRunning && !PlayerStats.underWater);
        player.SetBool("startWalk", isGrounded && !PlayerStats.underWater && Input.GetAxis("Vertical") != 0);
        player.SetBool("Right", isGrounded && !PlayerStats.underWater && Input.GetKey(KeyCode.D));
        player.SetBool("Left", isGrounded && !PlayerStats.underWater && Input.GetKey(KeyCode.A));
        player.SetBool("Swim", !isGrounded && PlayerStats.inWater);
    }

    public void Disable()
    {
        enabled = false;
        pr.enabled = false;
        isRunning = false;
        isWalking = false;
        isGrounded = false;
        pr.Clear();
    }
    public void Enable()
    {
        enabled = true;
        pr.enabled = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = (!PlayerStats.onShip &&  PlayerStats.celestial != null ? PlayerStats.celestial.gravity : false) || shipController.isGrounded;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > healthBar.maxVelocity && PlayerSettings.takeDamage)
        {
            healthBar.TakeDamage(Mathf.RoundToInt(collision.relativeVelocity.magnitude) - healthBar.maxVelocity);
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    public void CheckInput()
    {
        isWalking = (isGrounded || PlayerStats.inWater) && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0);
        isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;     
    }
    public void Teleport(Transform target)
    {
        transform.position = target.position;
    }
}
