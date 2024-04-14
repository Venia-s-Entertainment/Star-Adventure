using UnityEngine;
using UnityEngine.UI;
using Settings;

public class TemperatureManager : MonoBehaviour
{
    [SerializeField] HealthManager playerHealth;
    [SerializeField] Slider tempBar;
    [SerializeField] Text text;
    [SerializeField] Image Bar; 
    [SerializeField] float damageStrength = 0.1f;

    private float damage;
    private float currentTemp;
    private void Start()
    {
        tempBar.value = tempBar.maxValue / 4;
        currentTemp = tempBar.value;

        ChangeStatus();
    }
    // Update is called once per frame
    void Update()
    {
        float biggestNum = Mathf.Max(currentTemp, PlayerStats.Temperature);
        float smallestNum = Mathf.Min(currentTemp, PlayerStats.Temperature);

        float fill = (biggestNum - smallestNum) * 0.3f * (currentTemp > PlayerStats.Temperature ? -1 : 1);

        SetBarValue(fill * Time.deltaTime);
    }
    void ChangeStatus()
    {
        text.text = $"{Mathf.RoundToInt(currentTemp)}°C";
        Bar.color = Color.Lerp(new Color(0, 1, 1), Color.red, tempBar.value / tempBar.maxValue * 2);
    }
    void SetBarValue(float value)
    {
        currentTemp += value;
        tempBar.value = currentTemp;

        if (currentTemp >= PlayerSettings.maxTemp)
        {
            damage = currentTemp - PlayerSettings.maxTemp;

            playerHealth.TakeDamage(damage * damageStrength * Time.deltaTime);
        }
        if (currentTemp <= PlayerSettings.minTemp)
        {
            float biggestNum = Mathf.Max(tempBar.value, PlayerSettings.minTemp);
            float smallestNum = Mathf.Min(tempBar.value, PlayerSettings.minTemp);

            damage = biggestNum - smallestNum;

            playerHealth.TakeDamage(damage * damageStrength * Time.deltaTime);
        }

        ChangeStatus();
    }
}
