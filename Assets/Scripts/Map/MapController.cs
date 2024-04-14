using UnityEngine;
using Settings;
using System;

public class MapController : MonoBehaviour
{
    [SerializeField] GameObject POI;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject[] pointsOfInterest;
    [SerializeField] Camera mapCamera;
    [SerializeField] GameObject Icons;
    [SerializeField] float startZoom = 100000;
    [SerializeField] float maxZoom = 250000;
    [SerializeField] float minZoom = 20000;
    [SerializeField] Transform body;

    Camera mainCamera;
    public float zoomStrength;

    public bool mapIsEnabled;
    private bool locked;
    float defualtMinZoom;

    Vector3 AxisX;
    private void Start()
    {
        mainCamera = Camera.main;

        defualtMinZoom = minZoom;
    }
    void Update()
    {
        if (mapIsEnabled)
        {
            if (!locked)
            {
                transform.Translate(-Input.GetAxis("Horizontal") * zoomStrength * 10 * Time.deltaTime, 0, -Input.GetAxis("Vertical") * zoomStrength * 10 * Time.deltaTime);
            }

            if (Input.GetMouseButton(1))
            {
                transform.Rotate(0, Input.GetAxisRaw("Mouse X") * PlayerSettings.Sensivity, 0);

                AxisX += new Vector3(Input.GetAxis("Mouse Y") * PlayerSettings.Sensivity, 0, 0);
                AxisX.x = Mathf.Clamp(AxisX.x, -90, 90);
                body.localEulerAngles = AxisX;
            }

            zoomStrength = startZoom * 0.07f;

            startZoom += Input.mouseScrollDelta.y * zoomStrength;
         
            startZoom = Mathf.Clamp(startZoom, minZoom, maxZoom);

            transform.localScale = new Vector3(startZoom, startZoom, startZoom);
        }
    }
    public void HideAllPois()
    {
        foreach (var poi in pointsOfInterest)
        {
            poi.SetActive(false);
        }
    }
    public void OnTargetLocked(bool locked)
    {
        this.locked = locked;
    }
    public void GetMinimalZoom(CelestialInfo celestialInfo)
    {
        minZoom = locked ? celestialInfo.Radius * 3 : defualtMinZoom;
    }
    public void SetDefualtRotation()
    {
        transform.localEulerAngles = Vector3.zero;
    }
    public void OpenMap(bool enable)
    {
        mapIsEnabled = enable;

        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!enable);
        Cursor.visible = enable;

        mainCamera.gameObject.SetActive(!enable);

        HUD.SetActive(!enable);
        POI.SetActive(enable);
        Icons.SetActive(enable);
        mapCamera.gameObject.SetActive(enable);
    }
}
