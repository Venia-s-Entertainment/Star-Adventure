using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Settings;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField] PostProcessProfile ppProfile;
    [SerializeField] float wakingSpeed = 20;
    [SerializeField] Text timerText;
    [SerializeField] Text virtualMessage;
    [SerializeField] GameObject hud;
    [SerializeField] float timeSpeed = 5;

    HealthManager hm;
    PlayerController p;
    ColorGrading cg;
    float currentSecond = 0;
    int currentMinute = 0;
    float currentExposureValue = 0;
    public void Interact()
    {
        hm = FindObjectOfType<HealthManager>();
        p = FindObjectOfType<PlayerController>();
        cg = ppProfile.GetSetting<ColorGrading>();

        StartSleep();
    }

    private void StartSleep()
    {
        hud.SetActive(false);
        timerText.gameObject.SetActive(true);

        currentSecond = 0;
        currentMinute = 0;
        currentExposureValue = 0;

        GameSettings.SimulationSpeed = timeSpeed;

        virtualMessage.text = "Press F key to wake up";

        p.Disable();
        p.UpdateAnimation();

        StartCoroutine(GettingReady());
    }
    IEnumerator GettingReady()
    {
        while (cg.postExposure.value >= -10)
        {
            currentExposureValue -= wakingSpeed * Time.deltaTime;
            cg.postExposure.value = Mathf.RoundToInt(currentExposureValue);

            yield return new WaitForSeconds(0);
        }

        StartCoroutine(Sleeping());
    }
    IEnumerator Sleeping()
    {
        while (!Input.GetKeyDown(KeyCode.F))
        {
            hm.TakeDamage(-0.5f * Time.deltaTime);
            currentSecond += timeSpeed * Time.deltaTime;

            if (currentSecond >= 60)
            {
                currentMinute += 1;
                currentSecond = 0;
            }

            timerText.text = $"{currentMinute}:{Mathf.RoundToInt(currentSecond)}";

            yield return new WaitForSeconds(0);
        }

        StartCoroutine(WakingUp());
    }
    IEnumerator WakingUp()
    {
        while (cg.postExposure.value <= -1)
        {
            currentExposureValue += wakingSpeed * Time.deltaTime;
            cg.postExposure.value = Mathf.RoundToInt(currentExposureValue);

            yield return new WaitForSeconds(0);
        }

        StopSleep();
    }
    private void StopSleep()
    {
        StopAllCoroutines();

        p.Enable();
        hud.SetActive(true);
        timerText.gameObject.SetActive(false);

        virtualMessage.text = null;
        timerText.text = null;

        GameSettings.SimulationSpeed = 1;
    }
}
