using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    [SerializeField] HealthManager healthBar;
    [SerializeField] Slider oxygenBar;
    [SerializeField] Text text;
    [SerializeField] Image Bar;
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        oxygenBar.value = oxygenBar.maxValue;

        ChangeStatus();
    }
    private void Update()
    {
        if (!PlayerStats.canBreath)
            SetBarValue(PlayerStats.wearingAstroSuit ? -0.2f * Time.deltaTime : -4 * Time.deltaTime);
        else
            SetBarValue(20 * Time.deltaTime);
    }
    void ChangeStatus()
    {
        text.text = Mathf.RoundToInt(oxygenBar.value).ToString();
        Bar.color = Color.Lerp(Color.red, Color.green, oxygenBar.value / oxygenBar.maxValue);
    }
    public void SetBarValue(float value)
    {
        oxygenBar.value += value;

        if (oxygenBar.value <= 0)
            healthBar.TakeDamage(damage * Time.deltaTime);

        ChangeStatus();
    }
}
