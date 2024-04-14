using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField] Camera mapCamera;
    [SerializeField] MapController map;
    [SerializeField] Transform target;
    [SerializeField] float heighTreshold = 75;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + target.up * heighTreshold;
        transform.LookAt(mapCamera.transform);
        transform.localScale = Vector3.one * map.zoomStrength / 100;
    }
}
