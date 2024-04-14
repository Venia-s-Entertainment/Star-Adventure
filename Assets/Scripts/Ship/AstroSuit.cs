using UnityEngine;
using UnityEngine.Events;
using Settings;

public class AstroSuit : MonoBehaviour, IInteractable
{
    [SerializeField] OxygenManager oxygen;
    [SerializeField] GameObject regularSuit;
    [SerializeField] GameObject spaceSuit;
    [SerializeField] GameObject Tip;
    
    [Header("HUD")]
    [SerializeField] GameObject fuelBar;
    [SerializeField] JetPack jetPack;

    [SerializeField] UnityEvent OnInteract;

    bool isWearing = false;

    private float maxTemp;
    private float minTemp;
    void Start()
    {
        maxTemp = PlayerSettings.maxTemp;
        minTemp = PlayerSettings.minTemp;
    }
    public void Interact()
    {
        ToggleAstroSuit(!isWearing);
        OnInteract.Invoke();
    }
    private void ToggleAstroSuit(bool isWearing)
    {
        this.isWearing = isWearing;
        PlayerStats.wearingAstroSuit = isWearing;

        if (isWearing)
        {
            PlayerSettings.minTemp = -273;
            PlayerSettings.maxTemp = 500;
        }
        else
        {
            PlayerSettings.minTemp = minTemp;
            PlayerSettings.maxTemp = maxTemp;
        }
        spaceSuit.transform.localEulerAngles = new Vector3(-90, 0, 0);

        oxygen.SetBarValue(100);

        jetPack.ToggleJetPack(false);
        jetPack.fuelBar.value = jetPack.fuelBar.maxValue;
        jetPack.ChangeBarValue();

        Tip.SetActive(isWearing);
        spaceSuit.SetActive(!isWearing);
        regularSuit.SetActive(!isWearing);
        fuelBar.SetActive(isWearing);
    }
}
