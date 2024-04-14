using System;
using UnityEngine;
using Settings;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] PostProcessProfile postProcessProfile;
    [SerializeField] GameObject[] tips;
    [SerializeField] AudioMixer sounds;

    [Header("Debug")]
    [SerializeField] Console con;

    Bloom bloom;
    void Start()
    {
        con.Debug(PlayerPrefs.GetInt("FirstRun") != 0 ? "Loading saved settings..." : "Loading defualt settings...", "cyan");

        bloom = postProcessProfile.GetSetting<Bloom>();

        /////////////////////
        //Volume Settings////
        ChangeGlobalVolume(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetFloat("Global") : GameSettings.globalVolume);
        ChangeMusicVolume(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetFloat("Music") : GameSettings.musicVolume);
        ChangeEffectsVolume(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetFloat("Effects") : GameSettings.effectsVolume);
        /////////////////////
        //Graphics Settings//
        ChangeQualityLevel(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("QualityLevel") : GameSettings.qualityLevel);
        ChangeVsyncCount(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("Vsync") : 1);
        ChangeAntiAliasing(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("antiAliasing") : 1);
        SetScreenMode(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("screenMode") : 1);
        ChangeScreenResolution(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("screenResolution") : 4);
        SetBloom(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("Bloom") : 1);
        ChangeTargetFramerate(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("targetFps"): 1);
        ShowStarfield(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("Starfield") : 1);
        ////////////////////
        //Input Settings////
        ChangeSensivity(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetFloat("Sensivity") : 2);
        ShowTips(PlayerPrefs.GetInt("FirstRun") != 0 ? PlayerPrefs.GetInt("Tips") : 1);
        ////////////////////
        PlayerPrefs.SetInt("FirstRun", 1);
    }

    public void ChangeGlobalVolume(float value)
    {
        sounds.SetFloat("Global", value);
        PlayerPrefs.SetFloat("Global", value);
    }
    public void ChangeMusicVolume(float value)
    {
        sounds.SetFloat("Music", value);
        PlayerPrefs.SetFloat("Music", value);
    }
    public void ChangeEffectsVolume(float value)
    {
        sounds.SetFloat("Effects", value);
        PlayerPrefs.SetFloat("Effects", value);
    }
    public void ChangeQualityLevel(int Index)
    {
        QualitySettings.SetQualityLevel(Index);
        PlayerPrefs.SetInt("QualityLevel", Index);

        ChangeVsyncCount(PlayerPrefs.GetInt("Vsync"));
        ChangeAntiAliasing(PlayerPrefs.GetInt("antiAliasing"));
    }
    public void ChangeScreenResolution(int Index)
    {
        Screen.SetResolution(GameSettings.screenResolutions[Index].x, GameSettings.screenResolutions[Index].y, Screen.fullScreen);
        PlayerPrefs.SetInt("screenResolution", Index);
    }
    public void ChangeVsyncCount(int count)
    {
        QualitySettings.vSyncCount = count;
        PlayerPrefs.SetInt("Vsync", count);
    }
    public void ChangeAntiAliasing(int Index)
    {
        QualitySettings.antiAliasing = Index;
        PlayerPrefs.SetInt("antiAliasing", Index);
    }
    public void SetScreenMode(int Index)
    {
        Screen.fullScreen = Convert.ToBoolean(Index);
        PlayerPrefs.SetInt("screenMode", Index);
    }
    public void ChangeTargetFramerate(int Index)
    {
        Application.targetFrameRate = GameSettings.Framerates[Index];
        PlayerPrefs.SetInt("targetFps", Index);
    }
    public void SetBloom(int Active)
    {
        bloom.active = Convert.ToBoolean(Active);
        PlayerPrefs.SetInt("Bloom", Active);
    }
    public void ShowStarfield(int Active)
    {
        PlayerPrefs.SetInt("Starfield", Active);
    }
    public void ShowTips(int Active)
    {
        PlayerPrefs.SetInt("Tips", Active);

        if (CommandHandler.currentScene == 1)
        {
            foreach (GameObject tip in tips)
            {
                tip.GetComponent<Text>().enabled = Convert.ToBoolean(PlayerPrefs.GetInt("Tips"));
            }
        }
    }
    public void ShowFps(int Active)
    {
        PlayerPrefs.SetInt("Fps", Active);
    }
    public void ChangeSensivity(float value)
    {
        PlayerSettings.Sensivity = value;
        PlayerPrefs.SetFloat("Sensivity", value);
    }
}

