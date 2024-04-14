using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Settings;

public class HealthManager : MonoBehaviour
{
    [SerializeField] PostProcessProfile ppProfile;
    [SerializeField] PlayerDeathManager deathManager;
    public float maxVelocity = 15;

    private Vignette vignette;
    private ColorGrading colorGrading;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        vignette = ppProfile.GetSetting<Vignette>();
        colorGrading = ppProfile.GetSetting<ColorGrading>();

        PlayerSettings.currentHealth = PlayerSettings.maxHealth;
        ChangeStatus();
    }
    void ChangeStatus()
    {
        PlayerSettings.currentHealth = Mathf.Clamp(PlayerSettings.currentHealth, 0, PlayerSettings.maxHealth);
        vignette.intensity.value = (PlayerSettings.maxHealth - PlayerSettings.currentHealth) / PlayerSettings.maxHealth;
        colorGrading.saturation.value = (PlayerSettings.maxHealth - PlayerSettings.currentHealth) * -1;
    }
    public void TakeDamage(float damage)
    {
        if (PlayerSettings.takeDamage)
        {
            PlayerSettings.currentHealth -= damage;

            if (PlayerSettings.currentHealth <= 0 && !isDead)
            {
                deathManager.Kill();
                isDead = true;
            }
            ChangeStatus();
        }
    }
}
