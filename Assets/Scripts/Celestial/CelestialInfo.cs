using UnityEngine;

public class CelestialInfo : MonoBehaviour
{
    [Header("Celestial Body")]
    public Transform celestialBody;
    public Gravity gravity;

    [Header("Information")]
    public string Name;
    public float Radius;
    public float Mass;
    public bool isHabitable;

    [Header("Ambient Music")]
    [SerializeField] AmbientMusicController ambientMusic;
    [SerializeField] int trackNum;
    PlayerController p;

    Gravity[] gravityObjects;
    private void Start()
    {
        gravityObjects = FindObjectsOfType<Gravity>();

        Radius = celestialBody.localScale.x;

        p = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if (other.CompareTag("Player")) {
                if (PlayerStats.isPiloting)
                    other.transform.parent = transform;

                p.transform.parent = PlayerStats.onShip ? p.transform.parent : transform;

                PlayerStats.celestial = this;

                if (!PlayerStats.onShip)
                    PlayerStats.canBreath = isHabitable; 

                ambientMusic.SetNewClip(trackNum);

                foreach (Gravity gravObject in gravityObjects)
                {
                    if (gravObject != gravity)
                    {
                        gravObject.enabled = false;
                    }
                }
            }
            else {
                other.transform.parent = transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if (other.CompareTag("Player")) {
                if (PlayerStats.celestial != this)
                    return;
                if (PlayerStats.isPiloting)
                    other.transform.parent = null;

                p.transform.parent = PlayerStats.onShip ? p.transform.parent : null;

                PlayerStats.celestial = null;
                PlayerStats.Temperature = PlayerStats.onShip ? 25 : Space.Temp;
                PlayerStats.canBreath = PlayerStats.onShip || Space.canBreath;

                foreach (Gravity gravObject in gravityObjects)
                {
                    gravObject.enabled = true;
                }

                ambientMusic.SetNewClip(Space.spaceTrack);
            }
            else{
                other.transform.parent = null;
            }
        }
    }
}
