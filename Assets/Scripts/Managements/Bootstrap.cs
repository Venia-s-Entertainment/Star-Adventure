using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Settings;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] PostProcessProfile postProcess;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        GameSettings.SimulationSpeed = 1;
        AudioListener.volume = 1;

        postProcess.GetSetting<ColorGrading>().saturation.value = 0;
        postProcess.GetSetting<Vignette>().intensity.value = 0;
        postProcess.GetSetting<ColorGrading>().postExposure.value = 0;

        PlayerStats.inWater = false;
        PlayerStats.isPiloting = false;
        PlayerStats.underWater = false;
        PlayerStats.wearingAstroSuit = false;
        PlayerStats.onShip = false;

        GameSettings.isPaused = false;

        PlayerSettings.noclipEnabled = false;
    }

}
