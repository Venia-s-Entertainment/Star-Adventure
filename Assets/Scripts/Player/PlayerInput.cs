using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] JetPack jetPack;
    [SerializeField] Light flashLight;
    [SerializeField] MapController map;
    [SerializeField] SpaceShipController ssc;
    [SerializeField] InGameTerminal gameTerminal;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject mainHud;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject con;
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    void GetInput()
    {
        if (!Settings.GameSettings.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.F1))
                hud.SetActive(!hud.activeSelf);

            if (Input.GetKeyDown(KeyCode.J) && PlayerStats.wearingAstroSuit)
                jetPack.ToggleJetPack(!jetPack.IsActivated);

            if (Input.GetKeyDown(KeyCode.L) && PlayerStats.wearingAstroSuit)
                flashLight.gameObject.SetActive(!flashLight.gameObject.activeSelf);

            if (Input.GetKeyDown(KeyCode.M) && ssc.isControlling)
                map.OpenMap(!map.mapIsEnabled);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Pause(!Settings.GameSettings.isPaused);

        if (Input.GetKeyDown(KeyCode.F3))
            con.SetActive(!con.activeSelf);
    }
    public void Pause(bool Paused)
    {
        if (!map.mapIsEnabled || !gameTerminal.isOpened)
        {
            Cursor.visible = Paused;
            Cursor.lockState = (CursorLockMode)System.Convert.ToInt32(!Paused);
            mainHud.SetActive(!Paused);
        }

        AudioListener.volume = Paused ? 0 : 1;
        pauseMenu.SetActive(Paused);
        Time.timeScale = System.Convert.ToInt32(!Paused);

        Settings.GameSettings.isPaused = Paused;
    }
}
