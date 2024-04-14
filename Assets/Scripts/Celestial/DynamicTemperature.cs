using UnityEngine;

[RequireComponent(typeof(CelestialInfo))]
public class DynamicTemperature : MonoBehaviour
{
    private PlayerController player;

    [Header("Poles")]
    [SerializeField] Transform northPole;
    [SerializeField] Transform southPole;

    [Header("Temperature")]
    public float min;
    public float max;
    // Update is called once per frame
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if (PlayerStats.celestial == GetComponent<CelestialInfo>() && !PlayerStats.onShip)
        {
            float distanceToNorth = Vector3.Distance(northPole.position, player.transform.position);
            float distanceToSouth = Vector3.Distance(southPole.position, player.transform.position);

            float minDistance = Mathf.Min(distanceToNorth, distanceToSouth);

            PlayerStats.Temperature = (minDistance * (max - min) / (PlayerStats.celestial.Radius * 2 * Mathf.PI * 0.25f)) + min;
        }
    }
}
