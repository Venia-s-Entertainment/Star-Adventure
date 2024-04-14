using UnityEngine;
using UnityEngine.UI;

public class FpsManager : MonoBehaviour
{
    [SerializeField] Text fps;

    private int fpsCount;
    private float Second = 1.0f;

    private void Start()
    {
        SetActive();
    }
    void Update()
    {
        if (!Settings.GameSettings.isPaused)
        {
            if (Second < 0)
            {
                fps.text = $"FPS {fpsCount}";

                fpsCount = 0;
                Second = 1;
            }

            Second -= Time.deltaTime;
            fpsCount++;
        }
    }
    public void SetActive()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("FirstRun") != 0 ? System.Convert.ToBoolean(PlayerPrefs.GetInt("Fps")) : false);
    }
}
