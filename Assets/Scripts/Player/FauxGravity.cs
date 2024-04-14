using UnityEngine;

public class FauxGravity : MonoBehaviour
{
    private PlayerController p;
    private JetPack jetPack;
    private void Start()
    {
        p = FindObjectOfType<PlayerController>();
        jetPack = GetComponent<JetPack>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!PlayerStats.onShip || p.isGrounded)
        {
            if (PlayerStats.celestial && PlayerStats.celestial.gravity)
            {
                Attract(transform.parent, PlayerStats.celestial.celestialBody);
            }
        }
    }
    public void Attract(bool Enabled, Transform body)
    {
        if (Enabled)
        {
            Vector3 gravityUp = (transform.position - body.position).normalized;
            Vector3 bodyUp = transform.up;

            Quaternion rotation = Quaternion.FromToRotation(bodyUp, gravityUp) * transform.rotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.fixedDeltaTime);
        }
    }
}
