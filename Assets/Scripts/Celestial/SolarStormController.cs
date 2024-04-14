using UnityEngine;

public class SolarStormController : MonoBehaviour
{
    public bool isGoing = false;
    [SerializeField] GameObject[] magneticFields;
    [SerializeField] ParticleSystem solarStorm;
    [SerializeField] int maxTime = 2000;
    private float delayTime;

    [Header("Debug")]
    [SerializeField] Console con;
    // Start is called before the first frame update
    void Start()
    {
        ChangeDelayTime();

        con.Debug($"Storm will start in {delayTime} seconds", "white");
    }

    // Update is called once per frame
    void Update()
    {
        delayTime -= Settings.GameSettings.SimulationSpeed * Time.deltaTime;

        if (delayTime <= 0)
        {
            foreach (GameObject field in magneticFields)
            {
                field.SetActive(!field.activeSelf);

                ChangeDelayTime();
            }

            if (!solarStorm.isEmitting)
            {
                isGoing = true;

                solarStorm.Play();
            }
            else
            {
                isGoing = false;

                solarStorm.Stop();
                con.Debug($"Next storm in {delayTime} seconds", "white");
            }
        }
    }
    void ChangeDelayTime()
    {
        delayTime = Random.Range(0, maxTime);
    }
}
