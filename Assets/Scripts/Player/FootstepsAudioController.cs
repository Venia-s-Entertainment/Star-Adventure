using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsAudioController : MonoBehaviour
{
    [SerializeField] float delayTime = 0.1f;
    [SerializeField] PlayerController controller;

    [Header("Sounds")]
    [SerializeField] SoundsContainer GrassnSand;
    [SerializeField] SoundsContainer Barefoot;
    [SerializeField] SoundsContainer Water;
    [SerializeField] SoundsContainer Swim;
    [SerializeField] SoundsContainer Other;

    RaycastHit hit;

    private AudioSource audioPlayer;
    private float current;

    bool tagged;
    void Start()
    {
        current = delayTime;
        audioPlayer = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f))
        {
            if (!hit.collider.CompareTag("Grass&Sand"))
                tagged = false;
            else
                tagged = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (delayTime < 0 && controller.isWalking)
        {
            if (controller.isGrounded && !PlayerStats.underWater)
            {
                if (!PlayerStats.inWater)
                {
                    if (!tagged)
                        audioPlayer.clip = !PlayerStats.wearingAstroSuit ? Barefoot.GetRandomClip() : Other.GetRandomClip();
                    else
                        audioPlayer.clip = GrassnSand.GetRandomClip();
                }
                else 
                {
                    audioPlayer.clip = Water.GetRandomClip();
                }

                audioPlayer.PlayOneShot(audioPlayer.clip);

                delayTime = controller.isRunning ? current / 1.6f : current;
            }
            else if (PlayerStats.inWater)
            {
                audioPlayer.PlayOneShot(Swim.GetRandomClip());

                delayTime = (controller.isRunning ? current / 1.6f : current) * 2.5f;
            }
        }

        delayTime -= Time.deltaTime;
    }
}
