using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CelestialInfoWindow : MonoBehaviour
{
    [SerializeField] MapController mapBody;
    [SerializeField] NavIconsController targetPoint;
    [SerializeField] Text targetPointName;
    [SerializeField] WarpEngine warpEngine;
    [SerializeField] Image celestialImage;
    [SerializeField] Transform Arbrus;

    [Header("Information")]
    [SerializeField] Text Name;
    [SerializeField] Text distanceFromSun;
    [SerializeField] Text mass;
    [SerializeField] Text orbitalPeriod;
    [SerializeField] Text radius;
    [SerializeField] Text maxSurfaceTemp;
    [SerializeField] Text minSurfaceTemp;
    [SerializeField] Text IsHabitable;

    Transform celestialBody;
    CelestialInfo info;
    //Getting celestial body
    public void GetCelestial(Transform celestialBody)
    {
        this.celestialBody = celestialBody;

        MoveToCelestial();
    }
    //Getting information about celestial from CelestialInfo script
    public void GetInfo(CelestialInfo info)
    {
        this.info = info;
        Name.text = celestialBody.name;

        celestialImage.sprite = Resources.Load<Sprite>($"ReferenceImages/{Name.text}");

        distanceFromSun.text = $"{Mathf.RoundToInt(Vector3.Distance(info.transform.position, Arbrus.position))}(u)";
        mass.text = $"{info.Mass}(kilo)";
        orbitalPeriod.text = $"{Mathf.RoundToInt(360 / celestialBody.GetComponent<CelestialOrbit>().orbitSpeed / 60 / 24)}(days)";
        radius.text = $"{info.Radius}(u)";
        maxSurfaceTemp.text = $"{info.GetComponent<DynamicTemperature>().max}(°C)";
        minSurfaceTemp.text = $"{info.GetComponent<DynamicTemperature>().min}(°C)";
        IsHabitable.text = info.isHabitable ? "Yes" : "No";
    }
    public void SetTarget()
    {
        targetPointName.text = celestialBody.name;
        targetPoint.ShowTarget(info.transform);
        warpEngine.SetTarget(info.transform);
    }
    public void MoveToCelestial()
    {
        mapBody.transform.position = celestialBody.position;
    }
}
