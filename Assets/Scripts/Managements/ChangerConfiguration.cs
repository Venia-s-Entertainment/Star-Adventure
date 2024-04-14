using UnityEngine;
using UnityEngine.UI;

public class ChangerConfiguration : MonoBehaviour
{
    public string settingType;
    void Start()
    {
        if (GetComponent<Slider>())
            GetComponent<Slider>().value = PlayerPrefs.GetFloat(settingType);

        if (GetComponent<Dropdown>())
            GetComponent<Dropdown>().value = PlayerPrefs.GetInt(settingType);
    }
}
